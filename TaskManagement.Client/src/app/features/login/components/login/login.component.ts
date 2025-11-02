import { Component, inject } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';
import { Router } from '@angular/router';
import { AuthRequest } from '../../models/auth-request.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BaseComponent } from '../../../../shared/components/base/base.component';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent extends BaseComponent{
  private authService = inject(AuthenticationService);
  private router = inject(Router);

  isLogin: boolean = true;
  confirmPassword: string = '';

  loginData: AuthRequest = {
    email: '',
    password: ''
  };

  onLogin(form: any): void {
    if (form.invalid) {
      this.markFormGroupTouched(form);
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';

    this.authService.login(this.loginData).subscribe({
      next: (response) => {
        this.isLoading = false;
        if (response.validUser) {
          localStorage.setItem('validUser', 'true');

          this.router.navigate(['/task-register']);
        } else {
          this.errorMessage = 'Invalid credentials';
        }
      },
      error: (error) => {
        this.isLoading = false;
        this.errorMessage = error.message || 'Login failed. Please try again.';
      }
    });
  }

  onRegister(form: any): void {
    if (form.invalid) {
      this.markFormGroupTouched(form);
      return;
    }

    if (this.loginData.password !== this.confirmPassword) {
      this.errorMessage = 'Passwords do not match';
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';

    const registerData: AuthRequest = {
      email: this.loginData.email,
      password: this.loginData.password
    };

    this.authService.register(registerData).subscribe({
      next: (response) => {
        this.isLoading = false;
        if (response.validUser) {
          localStorage.setItem('validUser', 'true');
          this.router.navigate(['/task-register']); // Redirect same as login
        } else {
          this.errorMessage = 'Registration failed';
        }
      },
      error: (error) => {
        this.isLoading = false;
        this.errorMessage = error.message || 'Registration failed. Please try again.';
      }
    });
  }

  private markFormGroupTouched(form: any): void {
    Object.keys(form.controls).forEach(key => {
      const control = form.controls[key];
      control.markAsTouched();
    });
  }

  changeLoggingType(isExistingUser: boolean){
    this.isLogin = isExistingUser;
  }
}
