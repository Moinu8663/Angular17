import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginModelModule } from '../Models/login-model/login-model.module';
import { Observable } from 'rxjs';
import { UserModule } from '../Models/user/user.module';


@Injectable({
  providedIn: 'root'
})
export class LoginserviceService {

  constructor(private http: HttpClient) { }
  AddUser(userobj:LoginModelModule)
  {
    return this.http.post('https://localhost:7252/api/User/Login',userobj);
  }
  GetUsers():Observable<UserModule[]>
  {
    return this.http.get<UserModule[]>('https://localhost:7252/api/User');
  }
  GetUserByMobileNo(Mobile_No:string ):Observable<UserModule[]>
  {
    return this.http.get<UserModule[]>('https://localhost:7252/api/User/'+Mobile_No )
  }
  LoginUser( user:LoginModelModule){
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    return this.http.post('https://localhost:7252/api/User/Login',user,{headers});
  }
  
  }