import { Routes } from '@angular/router';
import { authRoutes } from '@features/auth/auth.routes';
import { profileRoutes } from '@features/profile/profile.routes';
import { authGuard } from '@core/guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('@features/public/landing-page/landing-page.component').then(
        (component) => component.LandingPageComponent,
      ),
  },
  {
    path: 'auth',
    loadChildren: () =>
      import('./features/auth/auth.routes').then(() => authRoutes),
  },
  {
    path: 'profile',
    canActivate: [authGuard],
    loadChildren: () =>
      import('./features/profile/profile.routes').then(() => profileRoutes),
  },
  {
    path: '**',
    loadComponent: () =>
      import('@features/public/page-not-found/page-not-found.component').then(
        (component) => component.PageNotFoundComponent,
      ),
  },
];
