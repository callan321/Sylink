import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '@core/services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormInputGroupComponent } from '@shared/components/form-input-group/form-input-group.component';

@Component({
  selector: 'app-reset-password',
  imports: [FormInputGroupComponent, ReactiveFormsModule],
  templateUrl: './reset-password.component.html',
  standalone: true,
  styleUrl: './reset-password.component.scss',
})
export class ResetPasswordComponent implements OnInit {
  private formBuilder = inject(FormBuilder);
  private authService = inject(AuthService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  email!: string;
  token!: string;

  resetPasswordForm = this.formBuilder.group({
    password: ['', Validators.required],
  });

  ngOnInit() {
    this.token = this.route.snapshot.queryParamMap.get('token') || '';
    this.email = this.route.snapshot.queryParamMap.get('email') || '';
  }

  onSubmit() {
    if (this.email && this.token && this.resetPasswordForm.valid) {
      const payload = {
        email: this.email,
        token: this.token,
        newPassword: this.resetPasswordForm.value.password!,
      };
      this.authService.resetPassword(payload).subscribe({
        next: (result) => {
          console.log(result);
          this.router
            .navigate(['/auth/login'])
            .then((result) => console.log(result));
        },
        error: (error) => console.log(error),
      });
    }
  }
}
