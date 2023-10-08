import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Route, Router } from '@angular/router';
import { PolicyService } from 'src/app/Services/policy.service';

@Component({
  selector: 'app-add-policy',
  templateUrl: './add-policy.component.html',
  styleUrls: ['./add-policy.component.css']
})
export class AddPolicyComponent {
  policyForm: FormGroup;
  policyNumber: any;
  chasisNumber: any;

  constructor(private formBuilder: FormBuilder ,private policySercice : PolicyService , private router : Router) {
    this.policyForm = this.formBuilder.group({
      policyNumber: ['', Validators.required],
      chasisNumber: ['', Validators.required]
    });
  }


  validatePolicy(){
    this.policyNumber=this.policyForm.get('policyNumber')?.value;
    this.policySercice.validatePolicy(this.policyNumber).subscribe({
      next: (response) => {
      if(response==true){
        this.validateChasisNumber()
      }
      else{
        alert("Invalid Policy Number");
      }
      },
      error: () => {
        alert("Invalid Policy Details!");
      }
    });
}
    
  

  validateChasisNumber(){
    this.chasisNumber=this.policyForm.get('chasisNumber')?.value;
    this.policySercice.validateChasis(this.chasisNumber).subscribe({
      next: (response) => {
      if(response==true){
        this.addPolicy();
      }
      else{
        alert("Invalid Chasis Number!");
      }
      },
      error: () => {
        alert("Invalid Policy Details!");
      }
    });
  }




  addPolicy(){

    this.policyNumber=this.policyForm.get('policyNumber')?.value;
    this.policySercice.addPolicy(this.policyNumber).subscribe({
      next: (response) => {
      if(response==true){
        this.router.navigateByUrl('/viewVehicle')
      }
      if(response==false){
        alert("Policy Already Exists!");
 
      }
      
      },
      error: () => {
        alert("Invalid Policy Details!");
      }
    });
}
}
