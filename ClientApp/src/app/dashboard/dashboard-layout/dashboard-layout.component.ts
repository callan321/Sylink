import { Component } from '@angular/core';
import {RouterOutlet} from '@angular/router';

@Component({
  selector: 'app-dashboard-layout',
  imports: [
    RouterOutlet
  ],
  templateUrl: './dashboard-layout.component.html',
  standalone: true,
  styleUrl: './dashboard-layout.component.css'
})
export class DashboardLayoutComponent {

}
