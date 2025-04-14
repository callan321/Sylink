import { Routes } from '@angular/router';
import { AppRoutes } from '@core/constants/app.routes';

export const authRoutes: Routes = [
  {
    path: AppRoutes.auth.route,
    loadComponent: () =>
      import('./auth-layout/auth-layout.component').then(
        (c) => c.AuthLayoutComponent,
      ),
    children: [
      {
        path: AppRoutes.auth.login,
        loadComponent: () =>
          import('./login/login.component').then((c) => c.LoginComponent),
      },
      {
        path: AppRoutes.auth.register,
        loadComponent: () =>
          import('./register/register.component').then(
            (c) => c.RegisterComponent,
          ),
      },
      {
        path: AppRoutes.auth.forgotPassword,
        loadComponent: () =>
          import('./forgot-password/forgot-password.component').then(
            (c) => c.ForgotPasswordComponent,
          ),
      },
      {
        path: AppRoutes.auth.resetPassword,
        loadComponent: () =>
          import('./reset-password/reset-password.component').then(
            (c) => c.ResetPasswordComponent,
          ),
      },
      {
        path: AppRoutes.auth.emailSent,
        loadComponent: () =>
          import('./email-sent/email-sent.component').then(
            (c) => c.EmailSentComponent,
          ),
      },
      {
        path: AppRoutes.auth.confirmEmail,
        loadComponent: () =>
          import('./confirm-email/confirm-email.component').then(
            (c) => c.ConfirmEmailComponent,
          ),
      },
    ],
  },
];
