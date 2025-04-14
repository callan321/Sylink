import { Component, inject, OnInit } from '@angular/core';
import { AuthService } from '@core/data-access/auth.service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { AuthStateService } from '@core/state/auth-state.service';
import { getAuthPath } from '@core/constants/app.routes';

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

  protected displayName = 'error';
  protected email = 'error';
  protected id = 'error';

  logout() {
    this.authState.logout(); // <-- use central logout
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
