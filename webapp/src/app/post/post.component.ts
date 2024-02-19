import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { PostserviceService } from '../Services/postservice.service';
import { AuthService } from '../Services/auth.service';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import {MatButtonModule} from '@angular/material/button';
import {MatDividerModule} from '@angular/material/divider';
import {MatCardModule} from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { AccountDetailsService } from '../Services/account-details.service';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatGridListModule } from '@angular/material/grid-list';


@Component({
  selector: 'app-post',
  standalone: true,
  imports: [MatCardModule, MatDividerModule, MatButtonModule, MatProgressBarModule,CommonModule,MatIconModule,MatFormFieldModule, MatInputModule,FormsModule, ReactiveFormsModule,MatGridListModule],
  providers:[HttpClient,PostserviceService,AuthService,AccountDetailsService],
  templateUrl: './post.component.html',
  styleUrl: './post.component.css'
})
export class PostComponent implements OnInit {
  post!:FormGroup;
  users!:any;
  Mobile_No!:any;
  postuser!:any;
  constructor(private formbuilder:FormBuilder,private postservice:PostserviceService,private auth:AuthService,private accdetails:AccountDetailsService){}
  ngOnInit(): void {
    this.post = this.formbuilder.group({
      Post : ['', [Validators.required]],
    });
    this.accdetails.GetAccountDetailsById(String(localStorage.getItem("Mobile_No"))).subscribe(
      (data:any) =>{
        console.log(data);
        this.users = data;
      }
    );
    this.postservice.GetPostDetailsById(String(localStorage.getItem("Mobile_No"))).subscribe(
      (data:any) =>{
        console.log(data);
        this.postuser= data;
      }
    );
    this.auth.canAuthenticate();
  }

  OnPost(){
    if (this.post.valid) {
      const userData = this.post.value;
      this.accdetails.GetAccountDetailsById(String(localStorage.getItem("Mobile_No"))).subscribe(
        data =>{
          console.log(data)
        }
      )
      this.postservice.AddPost(userData).subscribe(
        (response) => {
          console.log( response);
          localStorage.getItem("Mobile_No");
          alert("Post Upload Successfully");
          this.post.reset();
          
        }
      );
    }
  };
  OnUpdate(){
    if (this.post.valid) {
      const userData = this.post.value;
      this.postservice.UpdatePost(String(localStorage.getItem("Mobile_No")),userData).subscribe(
        (response) => {
          console.log( response);
          localStorage.getItem("Mobile_No");
          alert("Post Upload Successfully");
          this.post.reset();
        }
      );
    }
  };
  OnDelete(){
    if (this.post.valid) {
      const userData = this.post.value;
      this.postservice.DeletePost(String(localStorage.getItem("Mobile_No"))).subscribe(
        (response) => {
          console.log( response);
          localStorage.getItem("Mobile_No");
          alert("Post Upload Successfully");
          this.post.reset();
        }
      );
    }
  }

}
