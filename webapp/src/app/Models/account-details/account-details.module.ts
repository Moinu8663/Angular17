import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';



@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ]
})
export class AccountDetailsModule { 
  First_name!: string;
  Last_name!: string;
  Mobile_No!: string;
  Email!: string;
  DOB!: string;
  Address!: string;
  Bio!:string;
}
