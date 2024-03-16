import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { LoginserviceService } from '../Services/loginservice.service';
import { HttpClient } from '@angular/common/http';
import { passwordValidator, phoneValidator } from '../registration/registration.component';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../Services/auth.service';
import { MatButtonModule } from '@angular/material/button';
import {MatSelectModule} from '@angular/material/select';



@Component({
  selector: 'app-login',
  standalone: true,
  imports: [MatGridListModule,
    MatFormFieldModule,
     MatInputModule,
      MatIconModule,
      MatButtonModule,
      MatSelectModule,
      FormsModule,
       ReactiveFormsModule,
       CommonModule,
       RouterModule],
  providers:[LoginserviceService,HttpClient,AuthService],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {

  signin!: FormGroup;
  isLoggedIn!: boolean;
  constructor(private formBuilder: FormBuilder, private loginservice: LoginserviceService, private router: Router, private auth: AuthService) { }

  ngOnInit(): void {
    this.signin = this.formBuilder.group({
      Mobile_No: ['', [Validators.required, phoneValidator()]],
      Password: ['', [Validators.required, passwordValidator()]],
      Role:['', [Validators.required]]
    });
    this.auth.canAuthenticate();
  }
  onSubmit() {

    if (this.signin.valid) {

      const data = this.signin.value;

      this.loginservice.LoginUser(data).subscribe(
        (response) => {
          console.log(response);
          localStorage.setItem("Mobile_No", data.Mobile_No);

          // Handle success, e.g., show a success message to the user
          alert('Login Successful');
          this.signin.reset();
          this.router.navigate(['/profile']); // Navigate to the dashboard or desired page
        }
      );
    }
  }
}
