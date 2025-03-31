import { Component } from '@angular/core';
import {RouterOutlet} from '@angular/router';

@Component({
  selector: 'app-server-layout',
  imports: [
    RouterOutlet
  ],
  templateUrl: './server-layout.component.html',
  standalone: true,
  styleUrl: './server-layout.component.css'
})
export class ServerLayoutComponent {

}
