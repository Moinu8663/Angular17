import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserModule } from '../Models/user/user.module';

@Injectable({
  providedIn: 'root'
})
export class RegisterserviceService {

  constructor(private http: HttpClient) {}
  AddUser(userobj:UserModule)
  {
    return this.http.post("",userobj);
  }
  GetUsers()
  {
    return this.http.get("");
  }
  registerUser(user: UserModule) {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post('https://localhost:7252/api/User/Register', user, { headers });
  }
}
