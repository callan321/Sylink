import { Routes } from '@angular/router';
import { AppRoutes } from '@core/constants/app.routes';

export const profileRoutes: Routes = [
  {
    path: AppRoutes.profile.index,
    loadComponent: () =>
      import('./about/about.component').then((c) => c.AboutComponent),
  },
];
