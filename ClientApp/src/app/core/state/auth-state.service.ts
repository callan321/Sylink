import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { AuthService } from '@core/data-access/auth.service';
import { getAuthPath } from '@core/constants/app.routes';
import { IAuthenticatedUser } from '@core/interfaces/IAuthenticatedUser';

@Injectable({
  providedIn: 'root',
})
export class AuthStateService implements IAuthenticatedUser {
  private isAuthenticatedSubject = new BehaviorSubject<boolean>(false);

  get isAuthenticated(): boolean {
    return this.isAuthenticatedSubject.value;
  }

  constructor(
    private authService: AuthService,
    private router: Router,
  ) {}

  setAuthenticatedState(authenticated: boolean): void {
    this.isAuthenticatedSubject.next(authenticated);
  }

  reset(): void {
    this.setAuthenticatedState(false);
  }

  logout(): void {
    this.authService.logout().subscribe({
      next: () => {
        this.reset();
        this.router.navigate([getAuthPath('login')]);
      },
      error: (error) => {
        console.error('Logout failed:', error);
      },
    });
  }
}
