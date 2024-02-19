import { Component, NO_ERRORS_SCHEMA, OnInit } from '@angular/core';
import { LoginserviceService } from '../Services/loginservice.service';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterModule],
  providers: [LoginserviceService, HttpClient],
  schemas: [NO_ERRORS_SCHEMA],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

}


