import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AccountDetailsModule } from '../Models/account-details/account-details.module';

@Injectable({
  providedIn: 'root'
})
export class AccountDetailsService {

  constructor(private http:HttpClient) { }
  GetAccount(){
    return this.http.get('https://localhost:7095/api/Account')
  }
  GetAccountDetailsById(Mobile_No:string){
    return this.http.get('https://localhost:7095/api/Account/'+Mobile_No)
  }
  AddAccount(ADobj:AccountDetailsModule){
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    return this.http.post('https://localhost:7095/api/Account',ADobj,{headers})
  }
  UpdateAccount( Mobile_No:string, accountdetails:AccountDetailsModule){
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    return this.http.put('https://localhost:7095/api/Account/'+Mobile_No,accountdetails,{headers})
  }
}
