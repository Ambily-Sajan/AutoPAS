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
idv: any;
policyNumber: any;
vehicleType: any;
rtoname: any;
city: any;
state:any;
registrationNumber: any;
dateOfPurchase: any;
brand: any;
modelname: any;
variant: any;
bodyType: any;
fuelType: any;
transmissionType: any;
color:any;
chasisNumber:any;
engineNumber: any;
cubicCapacity: any;
seatingCapacity: any;
yearOfManufacture: any;
exShowroomPrice: any;


constructor(private policyService : PolicyService, private router : Router) {

}
ngOnInit() {
this.getPolicyDetails();
}

getPolicyDetails(){
  this.policyService.getPolicyDetails().subscribe({
    next: (response) => {
      this.policyData = response;
    },
    error: () => {
      alert("Invalid Login Credentials");
    }
  });
}

getVehicleDetails(event:any){
this.selectedPolicy=event.target.value;
  this.policyService.getVehicleDetails(this.selectedPolicy).subscribe({
    next: (response) => {
      this.vehicleData=response;
this.idv = response.idv;
this.policyNumber = response.policyNumber;
this.vehicleType = response.vehicleType;
this.rtoname = response.rtoname;
this.city = response.city;
this.state = response.state;
this.registrationNumber = response.registrationNumber;
this.dateOfPurchase = response.dateOfPurchase;
this.brand = response.brand;
this.modelname = response.modelname;
this.variant = response.variant;
this.bodyType = response.bodyType;
this.fuelType = response.fuelType;
this.transmissionType = response.transmissionType;
this.color = response.color;
this.chasisNumber = response.chasisNumber;
this.engineNumber = response.engineNumber;
this.cubicCapacity = response.cubicCapacity;
this.seatingCapacity = response.seatingCapacity;
this.yearOfManufacture = response.yearOfManufacture;
this.exShowroomPrice = response.exShowroomPrice;

      
    
      console.log(this.vehicleData)
    },
    error: () => {
      alert("");
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
  this.policyService.removePolicyNumber(this.selectedPolicy).subscribe({
    next: (response) => {  
      alert("Deleted Policy Number:"+this.selectedPolicy)
    },
    error: () => {
      alert("Delete Failed");
    }
  });
}

}
