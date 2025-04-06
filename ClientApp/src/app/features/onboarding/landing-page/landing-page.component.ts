import { Component, inject } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '@core/services/auth.service';

@Component({
  selector: 'app-landing-page',
  imports: [RouterLink],
  templateUrl: './landing-page.component.html',
  standalone: true,
  styleUrl: './landing-page.component.scss',
})
export class LandingPageComponent {
  private authService = inject(AuthService);
  private router = inject(Router);

  logout() {
    this.authService.logout().subscribe({
      next: (results) => {
        console.log(results);
        this.router.navigate(['/']).then(
          (result) => console.log('Navigation success:', result),
          (error) => console.error('Navigation error:', error),
        );
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
        this.router.navigate(['/auth/login']).then(
          (result) => console.log('Navigation success:', result),
          (error) => console.error('Navigation error:', error),
        );
      },
    });
  }
}
