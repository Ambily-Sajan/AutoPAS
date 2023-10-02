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

  constructor(private formBuilder: FormBuilder ,private policySercice : PolicyService , private router : Router) {
    this.policyForm = this.formBuilder.group({
      policyNumber: ['', Validators.required],
      chasisNumber: ['', Validators.required]
    });
  }

  addPolicy(){
    this.policySercice.addPolicy(this.policyForm.value).subscribe({
      next: (response) => {
      alert("Policy added Successfully")
      this.router.navigateByUrl('/home')
      },
      error: () => {
        alert("Invalid Policy Details!");
      }
    });
}
}
