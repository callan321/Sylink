import { Component, inject } from '@angular/core';
import { TextDividerComponent } from '@shared/components/text-divider/text-divider.component';
import { AuthButtonComponent } from '@shared/components/auth-button/auth-button.component';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AuthService } from '@core/data-access/auth.service';
import { Router, RouterLink } from '@angular/router';
import { FormInputGroupComponent } from '@shared/components/form-input-group/form-input-group.component';
import { FormErrorMessageComponent } from '@shared/components/form-error-message/form-error-message.component';

@Component({
  selector: 'app-register',
  imports: [
    TextDividerComponent,
    AuthButtonComponent,
    ReactiveFormsModule,
    FormInputGroupComponent,
    RouterLink,
    FormErrorMessageComponent,
  ],
  templateUrl: './register.component.html',
  standalone: true,
  styleUrl: './register.component.scss',
})
export class RegisterComponent {
  private formBuilder = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);
  protected fieldErrors: Record<string, string[]> = {};
  protected errorMessage: string = '';

  registerForm: FormGroup = this.formBuilder.group({
    email: ['', Validators.required],
    password: ['', Validators.required],
    displayName: ['', Validators.required],
  });

  onSubmit() {
    if (this.registerForm.valid) {
      const payload = this.registerForm.value;
      this.authService.register(payload).subscribe({
        next: (result) => {
          console.log(result);
          this.fieldErrors = {};
          this.errorMessage = '';
          this.router
            .navigate(['/auth/email-sent'], {
              queryParams: { email: payload.email },
            })
            .then((result) => console.log(result));
        },
        error: (errors) => {
          this.fieldErrors = errors.error.errors || {};
          this.errorMessage = errors.error.title;
          console.log(errors.error);
        },
      });
    } else {
      this.registerForm.markAllAsTouched();
    }
  }
}
