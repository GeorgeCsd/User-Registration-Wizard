import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegistrationWizardComponent } from './registration-wizard/registration-wizard.component';

@Component({
  selector: 'app-root', // Used in index.html <app-root></app-root>
  standalone: true, // This is a standalone component, no NgModule required
  imports: [CommonModule, RegistrationWizardComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {}
