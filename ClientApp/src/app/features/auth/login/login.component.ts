import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService, LoginRequest } from '@core/api-client';
import { AuthButtonComponent } from '@shared/components/auth-button/auth-button.component';
import { FormCheckboxComponent } from '@shared/components/form-checkbox/form-checkbox.component';
import { TextDividerComponent } from '@shared/components/text-divider/text-divider.component';
import { FormInputGroupComponent } from '@shared/components/form-input-group/form-input-group.component';
import { AppRoutes, getAuthPath } from '@core/constants/app.routes';

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
  ],
})
export class LoginComponent {
  private formBuilder = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);

  loginForm: FormGroup = this.formBuilder.group({
    email: ['', Validators.required],
    password: ['', Validators.required],
    rememberMe: [false],
  });

  onSubmit() {
    if (this.loginForm.valid) {
      const payload: LoginRequest = this.loginForm.value;
      this.authService.apiAuthLoginPost(payload).subscribe({
        next: () => this.router.navigate(['/']),
        error: (err) => console.error(err),
      });
    } else {
      this.loginForm.markAllAsTouched();
    }
  }

  protected readonly AppRoutes = AppRoutes;
  protected readonly getAuthPath = getAuthPath;
}
