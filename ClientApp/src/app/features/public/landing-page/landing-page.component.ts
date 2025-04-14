import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { getAuthPath, getProfilePath } from '@core/constants/app.routes';

@Component({
  selector: 'app-landing-page',
  imports: [RouterLink],
  templateUrl: './landing-page.component.html',
  standalone: true,
  styleUrl: './landing-page.component.scss',
})
export class LandingPageComponent {
  protected readonly getAuthPath = getAuthPath;
  protected readonly getProfilePath = getProfilePath;
}
