import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import {AuthService, FieldName, ResetPasswordRequest} from '@core/api-client';
import { AbstractFormComponent } from '@shared/form/abstract-form.component';
import { FormInputGroupComponent } from '@shared/components/form-input-group/form-input-group.component';
import { getAuthPath } from '@core/constants/app.routes';
import {FormErrorMessageComponent} from '@shared/components/form-error-message/form-error-message.component';

@Component({
  selector: 'app-reset-password',
  standalone: true,
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.scss',
  imports: [FormInputGroupComponent, ReactiveFormsModule, FormErrorMessageComponent],
})
export class ResetPasswordComponent extends AbstractFormComponent<ResetPasswordRequest> implements OnInit {
  private formBuilder = inject(FormBuilder);
  private authService = inject(AuthService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  email!: string;
  token!: string;

  resetPasswordForm = this.formBuilder.group({
    password: ''
  });

  ngOnInit(): void {
    this.email = this.route.snapshot.queryParamMap.get('email') || '';
    this.token = this.route.snapshot.queryParamMap.get('token') || '';
  }

  override getForm(): FormGroup {
    return this.resetPasswordForm;
  }

  override buildPayload(): ResetPasswordRequest {
    return {
      email: this.email,
      token: this.token,
      newPassword: this.resetPasswordForm.value.password!,
    };
  }

  override onSubmitSuccess(): void {
    this.router.navigate([getAuthPath('login')]);
  }

  onSubmit(): void {
    this.submitForm(payload => this.authService.apiAuthResetPasswordPost(payload));
  }

  protected readonly FieldName = FieldName;
}
