import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { environment } from 'src/app/environments/environment';
import { User } from '../Models/login.model';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  baseApiUrl: string= environment.baseApiUrl;


  constructor(private http:HttpClient) { }
  login(Credentials:User): Observable<User>{
    return this.http.post<User> (this.baseApiUrl+"/userlogin",Credentials)
  }

}
