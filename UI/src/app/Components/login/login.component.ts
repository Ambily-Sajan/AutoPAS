import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from 'src/app/Services/authentication.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  data: any;
  loginForm: FormGroup;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private authService: AuthenticationService,
    private formBuilder: FormBuilder
  ) {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }
  

  login() {

    this.authService.login(this.loginForm.value)
      .subscribe({
        next: (response) => {
          if (response) {
            localStorage.setItem('userId', response.userid)
            localStorage.setItem('token', Math.random().toString());
            console.log('token');
            alert('Logged In!')
            this.router.navigate(['home']);
          } 
        },
        error: () => {
          alert("Invalid Login Credentials");
        }
      });
  }
}
