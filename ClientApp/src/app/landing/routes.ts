import { Routes } from '@angular/router';
import {LandingLayoutComponent} from './landing-layout/landing-layout.component';
import {GetStartedComponent} from './get-started/get-started.component';

export default [
  {
    path: '',
    component: LandingLayoutComponent,
    children: [
      {
        path: '',
        component: GetStartedComponent,
      },
    ],
  },
] satisfies Routes;
