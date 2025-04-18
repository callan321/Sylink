import { inject } from '@angular/core';
import { AuthStateService } from '@core/state/auth-state.service';
import { AuthService } from '@core/api-client/api/auth.service';

export function initializeAuthState(): () => Promise<void> {
  return () => {
    const authService = inject(AuthService);
    const authState = inject(AuthStateService);

    return new Promise<void>((resolve) => {
      authService.apiAuthStatusGet().subscribe({
        next: (result) => {
          if (result.success) {
            authState.setAuthenticatedState(true);
          } else {
            authState.reset();
          }
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
