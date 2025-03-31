import { Routes } from '@angular/router';
import {ServerLayoutComponent} from './server-layout/server-layout.component';
import {ServerHomeComponent} from './server-home/server-home.component';


const routes: Routes = [
  {
    path: '',
    component: ServerLayoutComponent,
    children: [
      {
        path: '',
        component: ServerHomeComponent,
      },
    ],
  },
];

export default routes;
