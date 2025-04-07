import { Component, inject, OnInit } from '@angular/core';
import { Router, NavigationEnd, RouterOutlet } from '@angular/router';
import { filter } from 'rxjs';
import { AuthService } from '@core/data-access/auth.service';
import { AuthStateService } from '@core/state/auth-state.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  standalone: true,
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'ClientApp';

  private router = inject(Router);
  private authService = inject(AuthService);
  private authState = inject(AuthStateService);

  ngOnInit(): void {
    this.router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe(() => {
        // Call the backend to check the status of the user session
        this.authService.getStatus().subscribe({
          next: (result) => {
            // If the user is authenticated, update authState
            this.authState.setAuthenticatedState(true);
          },
          error: () => {
            // If there's an error or the session is invalid
            this.authState.reset();
          },
        });
      });
  }
}
