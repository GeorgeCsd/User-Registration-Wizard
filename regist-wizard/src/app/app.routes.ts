import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegistrationWizardComponent } from './registration-wizard/registration-wizard.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegistrationWizardComponent },
  { path: '', pathMatch: 'full', redirectTo: 'register' },
  { path: '**', redirectTo: 'register' }
];
