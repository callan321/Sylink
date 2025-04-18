import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService, ResetPasswordRequest } from '@core/api-client';
import { FormInputGroupComponent } from '@shared/components/form-input-group/form-input-group.component';
import {getAuthPath} from '@core/constants/app.routes';

@Component({
  selector: 'app-reset-password',
  standalone: true,
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.scss',
  imports: [FormInputGroupComponent, ReactiveFormsModule],
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
    this.email = this.route.snapshot.queryParamMap.get('email') || '';
    this.token = this.route.snapshot.queryParamMap.get('token') || '';
  }

  onSubmit() {
    if (this.email && this.token && this.resetPasswordForm.valid) {
      const payload: ResetPasswordRequest = {
        email: this.email,
        token: this.token,
        newPassword: this.resetPasswordForm.value.password!,
      };
      this.authService.apiAuthResetPasswordPost(payload).subscribe({
        next: () => this.router.navigate([getAuthPath('login')]),
        error: (err) => console.error(err),
      });
    }
  }
}
