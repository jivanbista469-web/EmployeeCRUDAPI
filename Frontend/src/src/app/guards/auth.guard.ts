import { inject, PLATFORM_ID } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { isPlatformBrowser } from '@angular/common';
import { AuthConstant } from '../constants/auth-constant';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const platformId = inject(PLATFORM_ID);

 if (!isPlatformBrowser(platformId)) {
    return true;
  }

  const token = localStorage.getItem(AuthConstant.ACCESS_TOKEN);
  if (token) {
    return true;
  }

  // 3. No token found on the client side -> Redirect to login using modern UrlTree return
  return router.parseUrl('/login');
};
