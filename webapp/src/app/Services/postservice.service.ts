import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PostModule } from '../Models/post/post.module';


@Injectable({
  providedIn: 'root'
})
export class PostserviceService {

  constructor(private http:HttpClient) { }
  Getpost(){
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    return this.http.get('https://localhost:7095/api/Account',{headers})
  }
  GetPostDetailsById(Mobile_No:string){
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    return this.http.get('https://localhost:7164/api/UserPost/'+Mobile_No,{headers})
  }
  AddPost(postobj:PostModule){
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    return this.http.post('https://localhost:7164/api/UserPost',postobj,{headers})
  }
  UpdatePost( Mobile_No:string, postmodules:PostModule){
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    return this.http.put('https://localhost:7164/api/UserPost/'+Mobile_No,postmodules,{headers})
  }
  DeletePost(Mobile_No:string){
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    return this.http.put('https://localhost:7164/api/UserPost/'+Mobile_No,{headers})
  }
}
