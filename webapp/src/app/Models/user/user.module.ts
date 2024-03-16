import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';



@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ]
})
export class UserModule {
  First_name!: string;
  Last_name!: string;
  Mobile_No!: string;
  Email!: string;
  Password!: string;
  Con_password!: string;
  Role!:string;
 }
