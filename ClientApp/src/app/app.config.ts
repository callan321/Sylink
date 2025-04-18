import {
  ApplicationConfig,
  provideAppInitializer,
  provideZoneChangeDetection,
} from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import {
  provideHttpClient,
  withFetch,
  withInterceptors,
} from '@angular/common/http';
import { authInterceptor } from '@core/interceptors/auth.interceptor';
import { initializeAuthState } from '@core/app.initializer';
import {Configuration} from '@core/api-client';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideHttpClient(withFetch(), withInterceptors([authInterceptor])),
    provideAppInitializer(initializeAuthState()),
    provideRouter(routes),
    {
      provide: Configuration,
      useFactory: () =>
        new Configuration({
          basePath: 'https://localhost:8000',
          withCredentials: true,
        }),
    },
  ],
};
