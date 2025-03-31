import {RouterModule, Routes} from '@angular/router';
import {MainLayoutComponent} from './layout/main-layout/main-layout.component';
import {NgModule} from '@angular/core';

export const routes: Routes = [
  {
    path: '',
    loadChildren: () =>
      import('./landing/routes').then(m => m.default),
  },
  {
    path: 'dashboard',
    component: MainLayoutComponent,
    loadChildren: () =>
      import('./dashboard/routes').then(m => m.default),
  },
  {
    path: 'server',
    component: MainLayoutComponent,
    children: [
      {
        path: '',
        loadChildren: () =>
          import('./server/routes').then(m => m.default),
      },
    ],
  },
  {
    path: '**',
    redirectTo: '',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
