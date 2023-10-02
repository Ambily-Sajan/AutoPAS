import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { environment } from 'src/app/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  baseApiUrl: string= environment.baseApiUrl;


  constructor(private http:HttpClient) { }
  login(response:any): Observable<any>{
    return this.http.post<any> (this.baseApiUrl+"/userlogin",response)
  }

}
