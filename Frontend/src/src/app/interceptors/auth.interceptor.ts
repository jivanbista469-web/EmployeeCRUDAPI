import { HttpInterceptorFn, HttpRequest, HttpHandlerFn, HttpErrorResponse } from '@angular/common/http';
import { AuthConstant } from '../constants/auth-constant';
import { inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { catchError, throwError } from 'rxjs';
import { Router } from '@angular/router';

export const authInterceptor: HttpInterceptorFn = (req: HttpRequest<unknown>, next: HttpHandlerFn) => {
    const platformId = inject(PLATFORM_ID);
    let token: string | null = null;
    const router = inject(Router);

    if (isPlatformBrowser(platformId)) {
        token = localStorage.getItem(AuthConstant.ACCESS_TOKEN);
    }

    let clonedReq = req;
    if (token) {
        clonedReq = req.clone({
            setHeaders: {
                Authorization: `Bearer ${token}`
            }
        });
    }

    return next(clonedReq).pipe(
        catchError((error: unknown) => {
            if (error instanceof HttpErrorResponse && error.status === 401) {
                if (isPlatformBrowser(platformId)) {
                    localStorage.removeItem(AuthConstant.ACCESS_TOKEN);
                }

                router.navigate(['/login']);
            }
            return throwError(() => error);
        })
    );
};
