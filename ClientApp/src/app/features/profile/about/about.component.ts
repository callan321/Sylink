import { Component, inject, OnInit } from '@angular/core';
import { AuthService } from '@core/data-access/auth.service';
import { Router, RouterLink } from '@angular/router';
import { AuthStateService } from '@core/state/auth-state.service';

@Component({
  selector: 'app-about',
  imports: [RouterLink],
  templateUrl: './about.component.html',
  standalone: true,
  styleUrl: './about.component.scss',
})
export class AboutComponent implements OnInit {
  private authService = inject(AuthService);
  private authState = inject(AuthStateService);
  private router = inject(Router);

  protected displayName = 'error';
  protected email = 'error';
  protected id = 'error';

  logout() {
    this.authService.logout().subscribe({
      next: () => {
        this.authState.reset();
        this.router.navigate(['/auth/login']);
      },
      error: (error) => {
        console.error(error);
      },
    });
  }

  ngOnInit(): void {
    this.authService.getStatus().subscribe({
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
