import { inject } from '@angular/core';
import { AuthStateService } from '@core/state/auth-state.service';
import { AuthService } from '@core/data-access/auth.service';

export function initializeAuthState(): () => Promise<void> {
  return () => {
    const authService = inject(AuthService);
    const authState = inject(AuthStateService);

    return new Promise((resolve) => {
      authService.getStatus().subscribe({
        next: (result) => {
          authState.setAuthenticatedState(true);
          resolve();
        },
        error: () => {
          authState.reset();
          resolve();
        },
      });
    });
  };
}
