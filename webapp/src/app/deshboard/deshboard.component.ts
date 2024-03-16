import { CUSTOM_ELEMENTS_SCHEMA, Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { Router, RouterModule } from '@angular/router';
import { ProfileComponent } from '../profile/profile.component';
import { MatIconModule } from '@angular/material/icon';
import { AuthService } from '../Services/auth.service';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-deshboard',
  standalone: true,
  imports: [MatSidenavModule,MatButtonModule,RouterModule,ProfileComponent,MatIconModule,CommonModule,RouterModule],
  schemas:[CUSTOM_ELEMENTS_SCHEMA],
  providers:[AuthService,HttpClient],
  templateUrl: './deshboard.component.html',
  styleUrl: './deshboard.component.css'
})
export class DeshboardComponent {
[x: string]: any;

isLoggedInUser: any;

  constructor(private router:Router,private auth:AuthService){}
  isLoggedIn() {
    return this.auth.isLoggedInUser();
  }
  login(Mobile_No:string,Password:string,Role:string){
    this.auth.login(Mobile_No,Password,Role);
  }
  logout(){
    this.auth.logout();
    localStorage.removeItem("Mobile_No",)
    this.router.navigate(['/login'])
  }
}
