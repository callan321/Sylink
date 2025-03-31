import {Routes} from '@angular/router';
import {DashboardLayoutComponent} from './dashboard-layout/dashboard-layout.component';
import {DashboardHomeComponent} from './dashboard-home/dashboard-home.component';

export default [
  {
    path: '',
    component: DashboardLayoutComponent,
    children: [
      {
        path: '',
        component: DashboardHomeComponent,
      },
    ],
  },
] satisfies Routes;
