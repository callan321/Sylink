import { Component, inject } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { FormInputGroupComponent } from '@shared/components/form-input-group/form-input-group.component';
import { AuthService } from '@core/services/auth.service';
import { Router, RouterLink } from '@angular/router';
import { TextDividerComponent } from '@shared/components/text-divider/text-divider.component';

@Component({
  selector: 'app-forgot-password',
  imports: [
    FormInputGroupComponent,
    ReactiveFormsModule,
    RouterLink,
    TextDividerComponent,
  ],
  templateUrl: './forgot-password.component.html',
  standalone: true,
  styleUrl: './forgot-password.component.scss',
})
export class ForgotPasswordComponent {
  private formBuilder = inject(FormBuilder);
  private authService = inject(AuthService);

  forgotPasswordForm: FormGroup = this.formBuilder.group({
    email: ['', Validators.required],
  });
  onSubmit() {
    if (this.forgotPasswordForm.valid) {
      const payload = this.forgotPasswordForm.value;
      this.authService.forgotPassword(payload).subscribe({
        next: (result) => {
          console.log(result);
        },
        error: ({ error }) => console.log(error),
      });
    }
  }
}
