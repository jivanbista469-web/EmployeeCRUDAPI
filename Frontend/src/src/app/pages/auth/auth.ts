import { Component, inject } from '@angular/core';
import { NonNullableFormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from './auth-service';
import { Router } from '@angular/router';
import { AuthRequest } from './model/auth-request.model';

@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [ReactiveFormsModule],
  styleUrl: './auth.scss',
  templateUrl: './auth.html',
})
export class Auth {
  private fb = inject(NonNullableFormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);

  errorMessage = '';

  // Swapped email for username with required validator
  loginForm = this.fb.group({
    userName: ['', [Validators.required]],
    password: ['', [Validators.required, Validators.minLength(6)]]
  });

  onSubmit(): void {
    if (this.loginForm.invalid)
       return;

    const credentials: AuthRequest = this.loginForm.getRawValue();
    
    this.authService.login(credentials).subscribe({
      next: (res) => {
        localStorage.setItem(AuthConstant.ACCESS_TOKEN, res.data.accessToken);
        this.authService.currentUserToken.set(res.data.accessToken);
        this.router.navigate(['/employee']);
      },
      error: (err) => {
        this.errorMessage = err.error?.message || 'Login failed';
      }
    });
  }
}
