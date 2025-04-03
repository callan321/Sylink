import { Component, inject } from '@angular/core';
import { AuthButtonComponent } from '@shared/components/auth-button/auth-button.component';
import { FormCheckboxComponent } from '@shared/components/form-checkbox/form-checkbox.component';
import { TextDividerComponent } from '@shared/components/text-divider/text-divider.component';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AuthService } from '@core/services/auth.service';
import { Router, RouterLink } from '@angular/router';
import { FormInputGroupComponent } from '@shared/components/form-input-group/form-input-group.component';

@Component({
  selector: 'app-login',
  imports: [
    AuthButtonComponent,
    FormCheckboxComponent,
    TextDividerComponent,
    FormsModule,
    ReactiveFormsModule,
    RouterLink,
    FormInputGroupComponent,
  ],
  templateUrl: './login.component.html',
  standalone: true,
  styleUrl: './login.component.scss',
})
export class LoginComponent {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);

  loginForm: FormGroup = this.fb.group({
    email: ['', Validators.required],
    password: ['', Validators.required],
  });

  onSubmit() {
    if (this.loginForm.valid) {
      const payload = this.loginForm.value;
      this.authService.login(payload).subscribe({
        next: (result) => {
          console.log(result);
          this.router.navigate(['/']).then((result) => console.log(result));
        },
      });
    } else {
      this.loginForm.markAllAsTouched();
    }
  }
}
