import { Component, inject } from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule} from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import {AuthService, FieldName, RegisterRequest} from '@core/api-client';
import { AbstractFormComponent } from '@shared/form/abstract-form.component';
import { FormInputGroupComponent } from '@shared/components/form-input-group/form-input-group.component';
import { FormErrorMessageComponent } from '@shared/components/form-error-message/form-error-message.component';
import { TextDividerComponent } from '@shared/components/text-divider/text-divider.component';
import { AuthButtonComponent } from '@shared/components/auth-button/auth-button.component';
import { FormSubmitButtonComponent } from '@shared/components/form-submit-button/form-submit-button.component';
import {  getAuthPath } from '@core/constants/app.routes';


@Component({
  selector: 'app-register',
  standalone: true,
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
  imports: [
    ReactiveFormsModule,
    RouterLink,
    FormInputGroupComponent,
    TextDividerComponent,
    AuthButtonComponent,
    FormSubmitButtonComponent,
    FormErrorMessageComponent,
  ],
})
export class RegisterComponent extends AbstractFormComponent {
  private formBuilder = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);
  protected readonly getAuthPath = getAuthPath;

  registerForm = this.formBuilder.group({
    email: '',
    password: '',
    displayName: '',
  });


  override getForm(): FormGroup {
    return this.registerForm;
  }

  override buildPayload(): RegisterRequest {
    return this.registerForm.getRawValue() as RegisterRequest;
  }

  override onSubmitSuccess(): void {
    const email = this.registerForm.value.email;
    this.router.navigate([getAuthPath('emailSent')], {
      queryParams: { email },
    });
  }

  onSubmit(): void {
    this.submitForm((payload) => this.authService.apiAuthRegisterPost(payload));
  }

  protected readonly FieldName = FieldName;
}
