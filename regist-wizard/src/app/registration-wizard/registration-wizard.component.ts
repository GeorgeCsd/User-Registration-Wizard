import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, Validators, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatStepper, MatStepperModule } from '@angular/material/stepper';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ApiService } from '../api.service';
import { IndustryDto, RegistrationRequest } from '../models';
import { MatDividerModule } from '@angular/material/divider';
import { Router } from '@angular/router';

@Component({
  standalone:true,
  selector:'app-registration-wizard',
  imports:[
    CommonModule,
    ReactiveFormsModule,
    MatToolbarModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatCheckboxModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
    MatStepperModule,
    MatDividerModule
  ],
  templateUrl:'./registration-wizard.component.html',
  styleUrl:'./registration-wizard.component.scss'
})
export class RegistrationWizardComponent implements OnInit{
   @ViewChild('stepper') stepper!: MatStepper;

   industries: IndustryDto[] = [];
   loadingIndustries = false;
   checkingUsername = false;
   usernameAvailable:boolean | null = null;
   canSubmit = false;
   companyForm:FormGroup;
   userForm:FormGroup;
   summaryForm:FormGroup;

   constructor(private formb: FormBuilder,
    private api: ApiService,private matsnack: MatSnackBar,private router: Router
   ){
    this.companyForm = this.formb.group({
      name:['',[Validators.required, Validators.minLength(2), Validators.maxLength(200)]],
      industryId:[null, [Validators.required]]
    });

    this.userForm = this.formb.group({
      name:['',[Validators.required, Validators.maxLength(120)]],
      firstName:['',[Validators.required, Validators.maxLength(120)]],
      userName:['',[Validators.required, Validators.maxLength(80)]],
      password:['',[Validators.required,Validators.minLength(8),Validators.maxLength(120),Validators.pattern(/^(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$/) ]],
      passwordRepeat:['',[Validators.required]],
      email:['',[Validators.email,Validators.maxLength(200)]]
    });

    this.summaryForm = this.formb.group({
      termsofServiceAccepted:[false,[Validators.requiredTrue]],
      privacyPolicyAccepted:[false,[Validators.requiredTrue]]
    });

   }

   /**
   * Initializes the component: loads required data.
   * @returns {void}
   */
  ngOnInit(): void {
    this.loadAllIndustries();
  }

  /**
   * Loads industries into state using the API.
   * @returns {void}
   */
  loadAllIndustries(): void{
    this.loadingIndustries = true;
    this.api.getIndustries().subscribe({
      next:(data) =>{
        this.industries = data;
        this.loadingIndustries = false;
      },
      error:()=>{
        this.loadingIndustries=false;
        this.matsnack.open("Failed to load industries from databaase","Close",{duration:3000});
      }
    })
  }

  /**
  * Resolves an industry name given its id.
  * @param {number} id
  * @returns {string} Display name or empty string if not found.
  */
  getIndustryName(id: number): string {
  const industry = this.industries.find(i => i.id === id);
  return industry ? industry.name : '';
}

 /**
  * Checks server-side availability of the entered username.
  * @returns {void}
  */ 
  checkUsername(): void{
    const formValue =(this.userForm.value.userName || '') as string;
    const username = formValue.trim();

    if(!username){
      this.usernameAvailable= null;
      return;
    }
    this.checkingUsername=true;
    this.api.checkUsername(username).subscribe({
      next:(available)=>{
        this.usernameAvailable = available;
        this.checkingUsername = false;
      },
      error:()=>{
        this.usernameAvailable=null;
        this.checkingUsername=false;
      }
    });

  }

  /**
  * Validates company form and advances the stepper.
  * @returns {void}
  */
  checkUserCompany(): void{
    if(this.companyForm.invalid){
      this.companyForm.markAllAsTouched();
      return;
    }
    this.stepper.next();

  }

  /**
  * Validates user form, ensures passwords match, and advances the stepper.
  * @returns {void}
  */
  checkUserForm(): void{
    if(this.userForm.invalid){
      this.userForm.markAllAsTouched();
      return;
    }
    const pswd=this.userForm.value.password as string;
    const psrdrpt=this.userForm.value.passwordRepeat as string;

    if(pswd !== psrdrpt){
      this.matsnack.open("Passwords do not match","Close",{duration:2500});
      return;
    }
    this.stepper.next();
  }

  /**
  * Goes back one step in the wizard.
  * @returns {void}
  */
  previousStep(): void{
    this.stepper.previous();
  }

  /**
  * Submits all forms: validates, builds payload, calls API,handles result and navigate to login 
  * page if succeful.
  * @returns {void}
  */
  submit(): void{
    this.companyForm.markAllAsTouched();
    this.userForm.markAllAsTouched();
    this.summaryForm.markAllAsTouched();

    if(this.companyForm.invalid || this.userForm.invalid || this.summaryForm.invalid){
      const terms=!!this.summaryForm.value.termsofServiceAccepted;
      const privacy=!!this.summaryForm.value.privacyPolicyAccepted;

      if(!terms && !privacy){
        this.matsnack.open("You must accept the Terms of Service and Privacy Policy","Close",{duration:3500});
      }
      else if(!terms){
        this.matsnack.open("You must accept the Terms of Service","Close",{duration:3500});
      }
      else if(!privacy){
        this.matsnack.open("You must accept the Privacy Policy","Close",{duration:3500});
      }
      else{
        this.matsnack.open("You must fix the highlighted fields","Close",{duration:3500});
      }
      return;
    }

    const payload : RegistrationRequest= {
      company:{
        name:(this.companyForm.value.name as string).trim(),
        industryId:Number(this.companyForm.value.industryId)
      },
      user:{
        name:(this.userForm.value.name as string).trim(),
        firstName:(this.userForm.value.firstName as string).trim(),
        userName:(this.userForm.value.userName as string).trim(),
        password:this.userForm.value.password as string,
        passwordRepeat:this.userForm.value.passwordRepeat as string,
        email:(this.userForm.value.email as string)?.trim() || null
      },
      summary:{
        termsofServiceAccepted: !!this.summaryForm.value.termsofServiceAccepted,
        privacyPolicyAccepted: !!this.summaryForm.value.privacyPolicyAccepted
      }
    };

    this.canSubmit= true;
    this.api.register(payload).subscribe({
      next:(res)=>{
        this.canSubmit= false;
        if(res.success){
          this.matsnack.open(res.message || "Your Registration is completed","Close",{duration:3000});
          this.companyForm.reset();
          this.userForm.reset();
          this.summaryForm.reset({termsofServiceAccepted: false,privacyPolicyAccepted: false});
          this.usernameAvailable=null;
          this.stepper.reset();
          this.router.navigate(['/login']);
        }
        else{
          this.matsnack.open(res.message || "Your registration has failed","Close",{duration:3500});
        }
      },
      error:(err)=>{
        this.canSubmit= false;
        const msg= err.error ?. message || err.error?. detail || err.error ?. title || (typeof err.error === 'string' ? err.error:null) || "Your registration has failed";
        this.matsnack.open(msg, "Close",{duration:3500});
        console.error(err);
      }
    })
  }
}