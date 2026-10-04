import { inject, PLATFORM_ID, Service, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { isPlatformBrowser } from '@angular/common';
import { OutputDataResponse } from '../../model/output-response.model';
import { Observable } from 'rxjs';
import { AuthResponse } from './model/auth-response.model';
import { AuthRequest } from './model/auth-request.model';

@Service()
export class AuthService {
    private apiUrl = `${environment.apiUrl}/Auth/Login`;
    private http = inject(HttpClient);

    private platformId = inject(PLATFORM_ID);

    currentUserToken = signal<string | null>(this.getToken());

    private getToken(): string | null {
        if (isPlatformBrowser(this.platformId)) {
            return localStorage.getItem(AuthConstant.ACCESS_TOKEN);
        }

        return null;
    }

    login(credentials: AuthRequest): Observable<OutputDataResponse<AuthResponse>> {
        return this.http.post<OutputDataResponse<AuthResponse>>(this.apiUrl, credentials);
    }

    logout(): void {
        if (isPlatformBrowser(this.platformId)) {
            localStorage.removeItem(AuthConstant.ACCESS_TOKEN);
        }
        this.currentUserToken.set(null);
    }
}
