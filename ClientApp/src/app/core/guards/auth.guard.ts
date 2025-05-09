import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthStateService } from '@core/state/auth-state.service';
import { getAuthPath } from '@core/constants/app.routes';

export const authGuard: CanActivateFn = (route, state) => {
  const authState = inject(AuthStateService);
  const router = inject(Router);

  if (authState.isAuthenticated) {
    return true;
  }
  return router.createUrlTree([getAuthPath('login')]);
};
