import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { environment } from 'src/app/environments/environment';
import { vehicleDetails } from '../Models/vehicledetails.model';
@Injectable({
  providedIn: 'root'
})
export class PolicyService {
  baseApiUrl: string= environment.baseApiUrl;


  constructor(private http:HttpClient) { }
  addPolicy(policynumber:number): Observable<boolean>{
    let userId =localStorage.getItem('userId')
    const formdata={
      userId:userId,
      policynumber: policynumber
    };
    return this.http.post<boolean> (this.baseApiUrl+"/api/customerPortal",formdata);
  }
  
  validatePolicy(policynumber:number):Observable<boolean>{
    return this.http.get<boolean>(this.baseApiUrl+"/api/customerPortal/validatePolicy/"+policynumber);
  }

  validateChasis(chasisnumber:string):Observable<boolean>{
    return this.http.get<boolean>(this.baseApiUrl+"/api/customerPortal/validateChasis/"+chasisnumber);

  }

getPolicyDetails(): Observable<number[]>{
  let userId =localStorage.getItem('userId')
  return this.http.get<number[]> (this.baseApiUrl+"/user/"+userId)
}

getVehicleDetails(policyNumber:vehicleDetails): Observable<vehicleDetails>{
  return this.http.get<vehicleDetails> (this.baseApiUrl+"/api/customerPortal/"+policyNumber)
}

removePolicyNumber(userId:number,policyNumber:number): Observable<boolean>{
  const data={
    userId:userId,
    policyNumber:policyNumber
  }
  return this.http.delete<boolean> (this.baseApiUrl+"/removepolicy/",{body:data});
}


}