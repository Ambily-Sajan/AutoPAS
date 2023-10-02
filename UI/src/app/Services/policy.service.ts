import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { environment } from 'src/app/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class PolicyService {
  baseApiUrl: string= environment.baseApiUrl;


  constructor(private http:HttpClient) { }
  addPolicy(formData:any): Observable<any>{
    let userId =localStorage.getItem('userId')
    return this.http.post<any> (this.baseApiUrl+"/api/customerPortal?userid="+userId+"&policynumber="+formData.policyNumber+"&chasisNumber="+formData.chasisNumber,null)
  }
  

getPolicyDetails(): Observable<any>{
  let userId =localStorage.getItem('userId')
  return this.http.get<any> (this.baseApiUrl+"/user/"+userId)
}

getVehicleDetails(policyNumber:any): Observable<any>{
  return this.http.get<any> (this.baseApiUrl+"/api/customerPortal/"+policyNumber)
}

removePolicyNumber(policyNumber:any): Observable<any>{
  return this.http.delete<any> (this.baseApiUrl+"/api/customerPortal/"+policyNumber)
}


}