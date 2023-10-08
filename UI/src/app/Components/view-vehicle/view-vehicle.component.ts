import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PolicyService } from 'src/app/Services/policy.service';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';


@Component({
  selector: 'app-view-vehicle',
  templateUrl: './view-vehicle.component.html',
  styleUrls: ['./view-vehicle.component.css']
})
export class ViewVehicleComponent implements OnInit {
policyData: any;
vehicleData:any;
currentPolicy:any;
selectedPolicy:any;
userId:any;
recordValidator:any;


constructor(private policyService : PolicyService, private router : Router) {

}
ngOnInit() {
this.getPolicyDetails();
this.userId=localStorage.getItem('userId');
}

getPolicyDetails(){
  this.policyService.getPolicyDetails().subscribe({
    next: (response) => {
      this.policyData = response;
      if(response==null){
        this.recordValidator=false;
      }
      else{
        this.recordValidator=true;
      }
    },
    error: () => {
     
    }
  });
}

getVehicleDetails(event:any){
this.selectedPolicy=event.target.value;
  this.policyService.getVehicleDetails(this.selectedPolicy).subscribe({
    next: (response) => {
      if(response!=null){
      this.vehicleData=response;    
      }
      else{
        alert("Invalid data");
      }
    },
    error: () => {
      alert("Error");
    }
  });
}

openConfirmation() {
  const result = window.confirm("Do you want to delete?");
  if (result) {
    this.deletePolicy();
  }
}

deletePolicy(){
  this.policyService.removePolicyNumber(this.userId,this.selectedPolicy).subscribe({
    next: (response) => {  
      this.router.navigate(['home']);
    },
    error: () => {
      alert("Delete Failed");
    }
  });
}

}
