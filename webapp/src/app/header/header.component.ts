import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import {MatToolbarModule} from '@angular/material/toolbar';
import { CommonModule } from '@angular/common';
import {MatMenuModule} from '@angular/material/menu';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterModule,MatToolbarModule, MatButtonModule, MatIconModule,MatMenuModule,CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {

}


