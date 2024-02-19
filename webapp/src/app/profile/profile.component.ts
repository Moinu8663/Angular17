import { Component, NO_ERRORS_SCHEMA, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AccountDetailsService } from '../Services/account-details.service';
import { MatGridListModule } from '@angular/material/grid-list';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, RouterModule,MatGridListModule,MatGridListModule],
  providers: [AccountDetailsService, HttpClient],
  schemas: [NO_ERRORS_SCHEMA],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {
  users: any;
  Mobile_No: any;
  constructor(private accountdetails:AccountDetailsService) { }
  ngOnInit(): void {
    this.accountdetails.GetAccountDetailsById(String(localStorage.getItem("Mobile_No"))).subscribe(
      (data:any) =>{
        console.log(data);
        this.users = data;
      }
    )
  }
}
