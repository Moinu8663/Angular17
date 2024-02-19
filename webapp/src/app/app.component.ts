import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./header/header.component";
import { FooterComponent } from "./footer/footer.component";
import { RegistrationComponent } from "./registration/registration.component";
import { HttpClientModule } from '@angular/common/http';
import { DeshboardComponent } from "./deshboard/deshboard.component";


@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    imports: [CommonModule, RouterOutlet, HeaderComponent, FooterComponent, RouterModule, RegistrationComponent, HttpClientModule, DeshboardComponent]
})
export class AppComponent {
  title = 'webapp';
}
