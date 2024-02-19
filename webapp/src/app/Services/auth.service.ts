import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  [x: string]: any;
   isLoggedIn!:boolean;

  constructor(private router:Router,private http:HttpClient) { }
  isAuthenticated():boolean
  {
    if (sessionStorage.getItem('token')!==null) {
      return true;
  }
  return false;
  }
  isLoggedInUser(): boolean {
    return this.isLoggedIn;
  }
  canAccess(){
    if (!this.isAuthenticated()) {
 
        this.router.navigate(['/login']);
    }
  }
  canAuthenticate(){
    if (this.isAuthenticated()) {
     
      this.router.navigate(['/home']);
    }
  }
  register(First_name:string,Mobile_No:string,Password:string){
       
    return this.http
     .post<{idToken:string}>(
       'https://localhost:7252/api/User/Register',
         {displayName:First_name,Mobile_No,Password}
     );
 }

 storeToken(token:string){
     sessionStorage.setItem('token',token);
 }

 login(Mobile_No:string,Password:string){
  
     return this.http.post<{idToken:string}>(
         'https://localhost:7252/api/User/Login',
           {Mobile_No,Password}
     ) ;
     
 }

 logout(){
  this.isLoggedInUser();
 }

 detail(){
   let token = sessionStorage.getItem('token');

   return this.http.post<{users:Array<{localId:string,displayName:string}>}>(
       'https://localhost:7252/api/User/Login',
       {idToken:token}
   );
 }

 removeToken(){
   sessionStorage.removeItem('token');
 }
}
