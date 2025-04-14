import { Routes } from '@angular/router';
import { AppRoutes } from '@core/constants/app.routes';
import { authRoutes } from '@features/auth/auth.routes';
import { profileRoutes } from '@features/profile/profile.routes';
import { authGuard } from '@core/guards/auth.guard';
import { redirectIfAuthenticatedGuard } from '@core/guards/redirect-if-authenticated.guard';

export const routes: Routes = [
  {
    path: AppRoutes.public.landing,
    loadComponent: () =>
      import('@features/public/landing-page/landing-page.component').then(
        (c) => c.LandingPageComponent,
      ),
  },
  {
    path: AppRoutes.auth.route,
    canActivate: [redirectIfAuthenticatedGuard],
    loadChildren: () =>
      import('@features/auth/auth.routes').then(() => authRoutes),
  },
  {
    path: AppRoutes.profile.route,
    canActivate: [authGuard],
    loadChildren: () =>
      import('@features/profile/profile.routes').then(() => profileRoutes),
  },
  {
    path: AppRoutes.public.notFound,
    loadComponent: () =>
      import('@features/public/page-not-found/page-not-found.component').then(
        (c) => c.PageNotFoundComponent,
      ),
  },
];
