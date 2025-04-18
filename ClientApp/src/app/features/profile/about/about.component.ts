import { Component, inject, OnInit } from '@angular/core';
import {  RouterLink } from '@angular/router';
import { AuthService } from '@core/api-client/api/auth.service';
import { AuthStateService } from '@core/state/auth-state.service';
import { OperationResultOfAuthStatusResponse } from '@core/api-client';

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
    this.authState.logout();
  }

  ngOnInit(): void {
    this.authService.apiAuthStatusGet().subscribe({
      next: (result: OperationResultOfAuthStatusResponse) => {
        console.log(result);
        if (result.success && result.data) {
          this.displayName = result.data.displayName ?? 'N/A';
          this.email = result.data.email ?? 'N/A';
          this.id = result.data.userId ?? 'N/A';
        } else {
          console.warn('Failed to load status:', result.message);
        }
      },
      error: (error) => {
        console.error('API error:', error);
      },
    });
  }
}
