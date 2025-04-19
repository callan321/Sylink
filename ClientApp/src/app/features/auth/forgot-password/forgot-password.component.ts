import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import {Router, RouterLink} from '@angular/router';
import {AuthService, FieldName, ForgotPasswordRequest} from '@core/api-client';
import { AbstractFormComponent } from '@shared/form/abstract-form.component';
import { FormInputGroupComponent } from '@shared/components/form-input-group/form-input-group.component';
import { TextDividerComponent } from '@shared/components/text-divider/text-divider.component';
import { getAuthPath } from '@core/constants/app.routes';
import {FormErrorMessageComponent} from '@shared/components/form-error-message/form-error-message.component';

@Component({
  selector: 'app-forgot-password',
  standalone: true,
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.scss',
  imports: [
    FormInputGroupComponent,
    ReactiveFormsModule,
    RouterLink,
    TextDividerComponent,
    FormErrorMessageComponent,
  ],
})
export class ForgotPasswordComponent extends AbstractFormComponent<ForgotPasswordRequest> {
  private formBuilder = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);
  protected readonly getAuthPath = getAuthPath;

  forgotPasswordForm: FormGroup = this.formBuilder.group({
    email: '',
  });

  override getForm(): FormGroup {
    return this.forgotPasswordForm;
  }

  override buildPayload(): ForgotPasswordRequest {
    return this.forgotPasswordForm.getRawValue();
  }

  override onSubmitSuccess(): void {
    const email = this.forgotPasswordForm.value.email;
    this.router.navigate([getAuthPath('emailSent')], {
      queryParams: { email },
    });
  }


  onSubmit(): void {
    this.submitForm(payload => this.authService.apiAuthForgotPasswordPost(payload));
  }

  protected readonly FieldName = FieldName;
}
