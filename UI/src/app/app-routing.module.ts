import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Components/login/login.component';
import { HomeComponent } from './Components/home/home.component';
import { AddPolicyComponent } from './Components/add-policy/add-policy.component';
import { ViewVehicleComponent } from './Components/view-vehicle/view-vehicle.component';
import { AuthGuard } from './Guards/auth-guard.guard';
import { unAuthGuard } from './Guards/un-auth.guard';

const routes: Routes = [
  { path: '', component: LoginComponent, canActivate: [unAuthGuard] }, 
  { path: 'home', component: HomeComponent,canActivate: [AuthGuard] }, 
  { path: 'addpolicy', component: AddPolicyComponent ,canActivate: [AuthGuard] }, 
  { path: 'viewVehicle', component: ViewVehicleComponent,canActivate: [AuthGuard] }, 

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
