import { Component, inject } from '@angular/core';
import { TextDividerComponent } from '@shared/components/text-divider/text-divider.component';
import { AuthButtonComponent } from '@shared/components/auth-button/auth-button.component';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AuthService } from '@core/services/auth.service';
import { Router, RouterLink } from '@angular/router';
import { FormInputGroupComponent } from '@shared/components/form-input-group/form-input-group.component';

@Component({
  selector: 'app-register',
  imports: [
    TextDividerComponent,
    AuthButtonComponent,
    ReactiveFormsModule,
    FormInputGroupComponent,
    RouterLink,
  ],
  templateUrl: './register.component.html',
  standalone: true,
  styleUrl: './register.component.scss',
})
export class RegisterComponent {
  private formBuilder = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);

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
          this.router
            .navigate(['/auth/login'])
            .then((result) => console.log(result));
        },
        error: (errors) => console.log(errors),
      });
    } else {
      this.registerForm.markAllAsTouched();
    }
  }
}
