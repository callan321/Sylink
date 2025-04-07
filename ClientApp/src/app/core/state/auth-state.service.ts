import { Injectable } from '@angular/core';
import { IAuthenticatedUser } from '@core/interfaces/IAuthenticatedUser';

@Injectable({
  providedIn: 'root',
})
export class AuthStateService implements IAuthenticatedUser {
  isAuthenticated = false;

  setAuthenticatedState(authenticated: boolean) {
    this.isAuthenticated = authenticated;
  }

  reset() {
    this.isAuthenticated = false;
  }
}
