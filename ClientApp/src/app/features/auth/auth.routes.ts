import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ForgotPasswordComponent } from '@features/auth/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from '@features/auth/reset-password/reset-password.component';
import { ConfirmEmailComponent } from '@features/auth/confirm-email/confirm-email.component';

export const authRoutes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'confirm-email', component: ConfirmEmailComponent },
  { path: 'forgot-password', component: ForgotPasswordComponent },
  { path: 'reset-password', component: ResetPasswordComponent },
];
