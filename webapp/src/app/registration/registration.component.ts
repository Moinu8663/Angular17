import { Component, OnInit } from '@angular/core';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatIconModule} from '@angular/material/icon';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import { AbstractControl, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { RegisterserviceService } from '../Services/registerservice.service';
import { CommonModule } from '@angular/common';
import { HttpClient, } from '@angular/common/http';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../Services/auth.service';
import { MatButtonModule } from '@angular/material/button';
import { AccountDetailsService } from '../Services/account-details.service';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [MatGridListModule,MatFormFieldModule, MatInputModule, MatIconModule,MatButtonModule,FormsModule, ReactiveFormsModule,CommonModule,RouterModule],
  providers:[RegisterserviceService,HttpClient,AuthService,AccountDetailsService],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css'
})
export class RegistrationComponent implements OnInit{

  signup!: FormGroup;
  // registrationError: any;
  constructor(private formBuilder: FormBuilder,private registrationService: RegisterserviceService,private router:Router,private auth:AuthService, private accdetails:AccountDetailsService) {}
ngOnInit(): void {
 this.signup = this.formBuilder.group({
      Email : ['', [Validators.required, Validators.email]],
      Mobile_No : ['', [Validators.required, phoneValidator()]],
      First_name : ['', [Validators.required]],
      Last_name : ['', [Validators.required]],
      Password : ['', [Validators.required, passwordValidator()]],
      Con_password :['', [Validators.required, ]]
 },{Validators:this.passwordMatchValidator});
 this.auth.canAuthenticate();
}
passwordMatchValidator(group: FormGroup | null) {
  const password = group?.get('password')?.value;
  const confirmPassword = group?.get('confirmPassword')?.value;
  return password === confirmPassword ? null : { mismatch: true };
}

onSubmit() {
  if (this.signup.valid) {
    const userData = this.signup.value;

    this.registrationService.registerUser(userData).subscribe(
      (response) => {
        console.log( response);
        localStorage.setItem("Mobile_No", userData.Mobile_No);
        // Handle success, e.g., show a success message to the user

        alert("Register Successfully")

        this.signup.reset();

        this.router.navigate(['/login']);

      }
    );
    this.accdetails.AddAccount(userData).subscribe(data =>{
      console.log(data);

   })
  }
}
}
export function passwordValidator(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    const value = control.value;
    if (value == null) {
      return { 'password': true, 'message': 'Password must be provided' };
  }
    const hasUpperCase = /[A-Z]/.test(value);
    const hasLowerCase = /[a-z]/.test(value);
    const hasDigit = /\d/.test(value);
    const isLengthValid = value.length >= 8;

    // Check password criteria
    if (hasUpperCase && hasLowerCase && hasDigit && isLengthValid) {
      return null; // Valid password
    } else {
      return { 'password': true, 'message': 'Password must contain at least one uppercase letter, one lowercase letter, one digit, and be at least 8 characters long' };
    }
  };
}

export function phoneValidator(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    const value: string = control.value;

    // Define your phone number validation logic here
    const isValid = /^\d{10}$/.test(value); // Example: Check if it's a 10-digit number

    // Return validation result with error message
    return isValid ? null : { 'phone': true, 'message': 'Invalid phone number. Please enter a 10-digit number' };
  };
}
