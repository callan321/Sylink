import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { AuthService, ForgotPasswordRequest } from '@core/api-client';
import { FormInputGroupComponent } from '@shared/components/form-input-group/form-input-group.component';
import { TextDividerComponent } from '@shared/components/text-divider/text-divider.component';
import { AppRoutes, getAuthPath } from '@core/constants/app.routes';

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
  ],
})
export class ForgotPasswordComponent {
  private formBuilder = inject(FormBuilder);
  private authService = inject(AuthService);

  forgotPasswordForm: FormGroup = this.formBuilder.group({
    email: ['', Validators.required],
  });

  onSubmit() {
    if (this.forgotPasswordForm.valid) {
      const payload: ForgotPasswordRequest = this.forgotPasswordForm.value;
      this.authService.apiAuthForgotPasswordPost(payload).subscribe({
        next: (result) => console.log(result),
        error: (error) => console.error(error),
      });
    }
  }

  protected readonly AppRoutes = AppRoutes;
  protected readonly getAuthPath = getAuthPath;
}
