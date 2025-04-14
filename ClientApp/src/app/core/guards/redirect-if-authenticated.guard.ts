import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthStateService } from '@core/state/auth-state.service';
import { getProfilePath } from '@core/constants/app.routes';

export const redirectIfAuthenticatedGuard: CanActivateFn = (route, state) => {
  const authState = inject(AuthStateService);
  const router = inject(Router);
  if (!authState.isAuthenticated) {
    return true;
  }
  return router.createUrlTree([getProfilePath('index')]);
};
