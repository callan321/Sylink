import { Component, inject, OnInit } from '@angular/core';
import { AuthService } from '@core/services/auth.service';
import { Router, RouterLink } from '@angular/router';
import { ProfileService } from '@core/services/profile.service';

@Component({
  selector: 'app-about',
  imports: [RouterLink],
  templateUrl: './about.component.html',
  standalone: true,
  styleUrl: './about.component.scss',
})
export class AboutComponent implements OnInit {
  private authService = inject(AuthService);
  private router = inject(Router);
  private profileService = inject(ProfileService);
  protected displayName = 'error';
  protected email = 'error';
  protected id = 'error';

  logout() {
    this.authService.logout().subscribe({
      next: (results) => {
        console.log(results);
      },
      error: (error) => {
        console.error(error);
      },
    });
  }

  refresh() {
    this.authService.refresh().subscribe({
      next: (results) => {
        console.log(results);
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  ngOnInit(): void {
    this.profileService.profile().subscribe({
      next: (results) => {
        console.log(results);
        this.displayName = results.data.displayName;
        this.email = results.data.email;
        this.id = results.data.id;
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
}
