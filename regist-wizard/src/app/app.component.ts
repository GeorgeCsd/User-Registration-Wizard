import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegistrationWizardComponent } from './registration-wizard/registration-wizard.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RegistrationWizardComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {}
