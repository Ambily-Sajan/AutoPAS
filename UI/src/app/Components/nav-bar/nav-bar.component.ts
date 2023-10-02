import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
userActive=false;
  constructor(private router : Router) { }
ngOnInit(): void {
  if(localStorage.getItem('token')){
    this.userActive=true;
  }
}

openConfirmation() {
  const result = window.confirm("Do you want to Logout?");
  if (result) {
    this.logOut();
  }
}
  logOut(){
localStorage.removeItem('token');
this.router.navigate(['']);


  }
}
