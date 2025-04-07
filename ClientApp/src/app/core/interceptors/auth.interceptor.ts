import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { AuthStateService } from '@core/state/auth-state.service';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authState = inject(AuthStateService);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status === 401) {
        authState.reset();
      }
      return throwError(() => error);
    }),
  );
};
