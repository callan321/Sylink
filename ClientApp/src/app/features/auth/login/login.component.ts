import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService, FieldName, LoginRequest } from '@core/api-client';
import { AbstractFormComponent } from '@shared/form/abstract-form.component';
import { AuthButtonComponent } from '@shared/components/auth-button/auth-button.component';
import { FormCheckboxComponent } from '@shared/components/form-checkbox/form-checkbox.component';
import { TextDividerComponent } from '@shared/components/text-divider/text-divider.component';
import { FormInputGroupComponent } from '@shared/components/form-input-group/form-input-group.component';
import { AppRoutes, getAuthPath } from '@core/constants/app.routes';
import {FormErrorMessageComponent} from '@shared/components/form-error-message/form-error-message.component';


@Component({
  selector: 'app-login',
  standalone: true,
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
  imports: [
    AuthButtonComponent,
    FormCheckboxComponent,
    TextDividerComponent,
    FormsModule,
    ReactiveFormsModule,
    RouterLink,
    FormInputGroupComponent,
    FormErrorMessageComponent,
  ],
})
export class LoginComponent extends AbstractFormComponent<LoginRequest> {
  private formBuilder = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);

  loginForm: FormGroup = this.formBuilder.group({
    email: '',
    password: '',
    rememberMe: false,
  });


  override getForm(): FormGroup {
    return this.loginForm;
  }

  override buildPayload(): LoginRequest {
    return this.loginForm.getRawValue();
  }

  override onSubmitSuccess(): void {
    this.router.navigate(['/']);
  }

  onSubmit(): void {
    this.submitForm(payload => this.authService.apiAuthLoginPost(payload));
  }

  protected readonly AppRoutes = AppRoutes;
  protected readonly getAuthPath = getAuthPath;
  protected readonly FieldName = FieldName;
}
