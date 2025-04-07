import { Routes } from '@angular/router';
import { authRoutes } from '@features/auth/auth.routes';
import { profileRoutes } from '@features/profile/profile.routes';
import { authGuard } from '@core/guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./features/onboarding/landing-page/landing-page.component').then(
        (c) => c.LandingPageComponent,
      ),
  },
  {
    path: 'auth',
    loadChildren: () =>
      import('./features/auth/auth.routes').then((r) => authRoutes),
  },
  {
    path: 'profile',
    canActivate: [authGuard],
    loadChildren: () =>
      import('./features/profile/profile.routes').then((r) => profileRoutes),
  },
  {
    path: '**',
    loadComponent: () =>
      import('./core/components/not-found/not-found.component').then(
        (c) => c.NotFoundComponent,
      ),
  },
];
