import { Component, OnInit } from '@angular/core';
import { ContectusService } from '../Services/contectus.service';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { phoneValidator } from '../registration/registration.component';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { HttpClient } from '@angular/common/http';
import { RouterModule } from '@angular/router';


@Component({
  selector: 'app-contectus',
  standalone: true,
  imports: [MatGridListModule,MatFormFieldModule, MatInputModule, MatIconModule,MatButtonModule,FormsModule, ReactiveFormsModule,CommonModule,RouterModule],
  providers:[HttpClient,ContectusService],
  templateUrl: './contectus.component.html',
  styleUrl: './contectus.component.css'
})
export class ContectusComponent implements OnInit{

  contect!:FormGroup
  constructor(private formbuilder:FormBuilder, private contectus:ContectusService){}
  ngOnInit(): void {
    this.contect = this.formbuilder.group({
      Name : ['', [Validators.required]],
      Email : ['', [Validators.required, Validators.email]],
      Mobile_No : ['', [Validators.required, phoneValidator()]],
      Message : ['', [Validators.required]],
 })
  }
  Onsubmit(){
    if (this.contect.valid) {
      const userData = this.contect.value;
  
      this.contectus.AddMessage(userData).subscribe(
        (response) => {
          console.log( response);
          localStorage.setItem("Mobile_No", userData.Mobile_No);
          alert("Message send Successfully");
          this.contect.reset();
        }
      );
    }
  }
}
