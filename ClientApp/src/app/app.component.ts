import { Component, inject, OnInit } from '@angular/core';
import { Router, NavigationEnd, RouterOutlet } from '@angular/router';
import { filter } from 'rxjs';
import { AuthService } from '@core/api-client';
import { AuthStateService } from '@core/state/auth-state.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
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
        this.authService.apiAuthStatusGet().subscribe({
          next: (result) => {
            if (result.success) {
              this.authState.setAuthenticatedState(true);
            } else {
              this.authState.reset();
            }
          },
          error: () => {
            this.authState.reset();
          },
        });
      });
  }
}
