import { ApplicationConfig, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(), // Registers a handler for uncaught global errors
    provideZoneChangeDetection({ eventCoalescing: true }),  // Optimizes change detection by coalescing events
    provideRouter(routes) // Sets up routing with defined application routes
  ]
};
