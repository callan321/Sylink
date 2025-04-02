import { Routes } from '@angular/router';
import {authRoutes} from './features/auth/auth.routes';


export const routes: Routes = [
  {
    path: '',
    loadComponent : () => import('./features/onboarding/landing-page/landing-page.component').then(m => m.LandingPageComponent) ,
  },
  {
    path : 'auth',
    loadChildren: () =>  import('./features/auth/auth.routes').then(r => authRoutes)
  }
];
