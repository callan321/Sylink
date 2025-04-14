import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ForgotPasswordComponent } from '@features/auth/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from '@features/auth/reset-password/reset-password.component';
import { ConfirmEmailComponent } from '@features/auth/confirm-email/confirm-email.component';
import { EmailSentComponent } from '@features/auth/email-sent/email-sent.component';
import { AuthLayoutComponent } from '@features/auth/auth-layout/auth-layout.component';

export const authRoutes: Routes = [
  {
    path: '',
    component: AuthLayoutComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'email-sent', component: EmailSentComponent },
      { path: 'confirm-email', component: ConfirmEmailComponent },
      { path: 'forgot-password', component: ForgotPasswordComponent },
      { path: 'reset-password', component: ResetPasswordComponent },
    ],
  },
];
