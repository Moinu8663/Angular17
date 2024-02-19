import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { RegistrationComponent } from './registration/registration.component';
import { LoginComponent } from './login/login.component';
import { ProfileComponent } from './profile/profile.component';
import { AboutComponent } from './about/about.component';
import { ContectusComponent } from './contectus/contectus.component';
import { UpdateProfileComponent } from './update-profile/update-profile.component';
import { TimelineComponent } from './timeline/timeline.component';
import { PostComponent } from './post/post.component';

export const routes: Routes = [
    {path:'',component:HomeComponent},
    {path:'home',component:HomeComponent},
    {path:'registration',component:RegistrationComponent},
    {path:'login',component:LoginComponent},
    {path:'profile',component:ProfileComponent},
    {path:'about',component:AboutComponent},
    {path:'contectus',component:ContectusComponent},
    {path:'updateprofile',component:UpdateProfileComponent},
    {path:'timeline',component:TimelineComponent},
    {path:'post',component:PostComponent}
];
