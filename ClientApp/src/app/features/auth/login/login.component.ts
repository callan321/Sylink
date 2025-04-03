import { Component } from '@angular/core';
import { AuthButtonComponent } from '../../../shared/components/auth-button/auth-button.component';
import { FormCheckboxComponent } from '../../../shared/components/form-checkbox/form-checkbox.component';
import { TextDividerComponent } from '../../../shared/components/text-divider/text-divider.component';

@Component({
  selector: 'app-login',
  imports: [AuthButtonComponent, FormCheckboxComponent, TextDividerComponent],
  templateUrl: './login.component.html',
  standalone: true,
  styleUrl: './login.component.scss',
})
export class LoginComponent {}
