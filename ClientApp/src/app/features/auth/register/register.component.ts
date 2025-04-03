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
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [TextDividerComponent, AuthButtonComponent, ReactiveFormsModule],
  templateUrl: './register.component.html',
  standalone: true,
  styleUrl: './register.component.scss',
})
export class RegisterComponent {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);

  registerForm: FormGroup = this.fb.group({
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
