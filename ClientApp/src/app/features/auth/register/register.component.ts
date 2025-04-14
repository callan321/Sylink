import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '@core/data-access/auth.service';
import { AbstractFormComponent } from '@shared/form/abstract-form.component';
import { FormInputGroupComponent } from '@shared/components/form-input-group/form-input-group.component';
import { FormErrorMessageComponent } from '@shared/components/form-error-message/form-error-message.component';
import { TextDividerComponent } from '@shared/components/text-divider/text-divider.component';
import { AuthButtonComponent } from '@shared/components/auth-button/auth-button.component';
import { FormSubmitButtonComponent } from '@shared/components/form-submit-button/form-submit-button.component';
import { AppRoutes, getAuthPath } from '@core/constants/app.routes';
import { TypedFormGroup } from '@shared/form/types';

type RegisterFormFields = {
  email: string;
  password: string;
  displayName: string;
};

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RouterLink,
    FormInputGroupComponent,
    FormErrorMessageComponent,
    TextDividerComponent,
    AuthButtonComponent,
    FormSubmitButtonComponent,
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
})
export class RegisterComponent extends AbstractFormComponent {
  private formBuilder = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);

  registerForm = this.formBuilder.nonNullable.group<RegisterFormFields>({
    email: '',
    password: '',
    displayName: '',
  });

  override getForm(): TypedFormGroup<RegisterFormFields> {
    return this.registerForm;
  }

  override buildPayload(): RegisterFormFields {
    return this.registerForm.value as RegisterFormFields;
  }

  override onSubmitSuccess(): void {
    const email = this.registerForm.value.email;
    this.router.navigate([getAuthPath('emailSent')], {
      queryParams: { email },
    });
  }

  onSubmit(): void {
    this.submitForm((payload) => this.authService.register(payload));
  }

  protected readonly AppRoutes = AppRoutes;
}
