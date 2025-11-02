import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { AuthRequest } from '../models/auth-request.model';
import { AuthResponse } from '../models/auth-response.model';
import { catchError, Observable } from 'rxjs';
import { BaseService } from '../../../shared/services/base.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService extends BaseService {
  private readonly baseUrl = environment.apiUrl;

  private httpClient = inject(HttpClient);

  login(loginData: AuthRequest): Observable<AuthResponse> {
    const url = `${this.baseUrl}/login`;

    return this.httpClient.post<AuthResponse>(url, loginData)
      .pipe(
        catchError(this.handleError)
      );
  }

  register(registerData: AuthRequest): Observable<AuthResponse> {
    const url = `${this.baseUrl}/register`;

    return this.httpClient.post<AuthResponse>(url, registerData)
      .pipe(
        catchError(this.handleError)
      );
  }
}
