import { Component, OnInit } from '@angular/core';
import { ContectusService } from '../../Services/contectus.service';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatGridListModule } from '@angular/material/grid-list';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-contectinfo',
  standalone: true,
  imports: [MatGridListModule,RouterModule,CommonModule,MatButtonModule,MatCardModule],
  providers:[ContectusService,HttpClient],
  templateUrl: './contectinfo.component.html',
  styleUrl: './contectinfo.component.css'
})
export class ContectinfoComponent implements OnInit {
contect:any;
Mobile_No:any;
constructor(private contectus:ContectusService){}
  ngOnInit(): void {
    this.contectus.GetMessage().subscribe(
      (data:any) =>{
        console.log(data);
        this.contect = data;
      }
    )
  }
}
