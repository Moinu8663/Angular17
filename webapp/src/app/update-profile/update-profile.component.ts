import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AccountDetailsService } from '../Services/account-details.service';
import { AuthService } from '../Services/auth.service';
import { AccountDetailsModule } from '../Models/account-details/account-details.module';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { Router, RouterModule } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import {MatDatepickerModule} from '@angular/material/datepicker';



@Component({
  selector: 'app-update-profile',
  standalone: true,
  imports: [MatGridListModule,MatFormFieldModule, MatInputModule, MatIconModule,MatButtonModule,FormsModule, ReactiveFormsModule,CommonModule,RouterModule,MatDatepickerModule],
  providers:[HttpClient,AccountDetailsService,AuthService,],
  templateUrl: './update-profile.component.html',
  styleUrl: './update-profile.component.css'
})
export class UpdateProfileComponent implements OnInit 
{
  update!: FormGroup;
  accdetailsmodules!:AccountDetailsModule
  user!:any
  Mobile_No:any
  constructor(private form:FormBuilder,private accdetails:AccountDetailsService,private auth:AuthService,private router:Router){}
 ngOnInit(): void {
  this.update = this.form.group({
    First_name:[''],
  Email:[''],
  DOB:[''],
  Address:[''],
  Bio:['']
  });
  this.auth.canAuthenticate();
  }
  Onupdate() {
    if (this.update.valid) 
    {
        const data = this.update.value;

        this.accdetails.UpdateAccount(String(localStorage.getItem("Mobile_No")),data).subscribe(data => 
      {
        console.log(data);
        this.user =data;
        localStorage.setItem("Mobile_No",this.Mobile_No);
      })
      alert("Update Successfully")
        this.update.reset();
        this.router.navigate(['/profile']);

    }
  }
}


