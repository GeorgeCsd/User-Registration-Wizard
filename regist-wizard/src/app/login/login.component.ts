import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiService } from '../api.service';

@Component({
  standalone: true,
  selector: 'app-login',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent{
  loading = false;
  error: string | null = null;
  success: string | null = null;  
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private api: ApiService
  ) {
    this.form = this.fb.group({
      userName: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }

  /**
  * Submits the login form, validates input, and calls the API to authenticate the user.
  * @returns {void}
  */
  submit(): void{
    this.error = null;
    this.success = null;  
    if (this.form.invalid || this.loading) return;

    this.loading = true;

    this.api.login(this.form.value as { userName: string; password: string }).subscribe({
      next: res =>{
        if (res?.success){
          this.success = '✅ Login is completed!'; 
        } else{
          this.error = res?.message || 'Login failed';
        }
        this.loading = false;
      },
      error: err =>{
        this.error = err?.error?.message || 'Invalid username or password';
        this.loading = false;
      }
    });
  }
}