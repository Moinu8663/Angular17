import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ContectusModule } from '../Models/contectus/contectus.module';

@Injectable({
  providedIn: 'root'
})
export class ContectusService {

  constructor(private http:HttpClient) { }
  AddMessage(contectusobj:ContectusModule){
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    return this.http.post('https://localhost:7298/api/ContectUs',contectusobj,{headers})
  }
}
