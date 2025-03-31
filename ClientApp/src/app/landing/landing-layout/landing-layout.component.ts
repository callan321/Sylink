import { Component } from '@angular/core';
import {RouterOutlet} from '@angular/router';

@Component({
  selector: 'app-landing-layout',
  imports: [
    RouterOutlet
  ],
  templateUrl: './landing-layout.component.html',
  standalone: true,
  styleUrl: './landing-layout.component.css'
})
export class LandingLayoutComponent {

}
