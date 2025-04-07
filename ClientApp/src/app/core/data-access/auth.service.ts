import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '@environment/environment';

export interface RegisterPayload {
  email: string;
  password: string;
  displayName: string;
}

export interface LoginPayload {
  email: string;
  password: string;
  rememberMe: boolean;
}

export interface ConfirmEmailPayload {
  email: string;
  token: string;
}

export interface ForgotPasswordPayload {
  email: string;
}

export interface ResetPasswordPayload {
  email: string;
  token: string;
  newPassword: string;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly jsonHeaders = new HttpHeaders({
    'Content-Type': 'application/json',
  });

  private readonly publicRequestOptions = {
    headers: this.jsonHeaders,
  };

  private readonly secureRequestOptions = {
    headers: this.jsonHeaders,
    withCredentials: true,
  };

  private readonly baseUrl = `${environment.apiUrl}/Auth`;

  constructor(private http: HttpClient) {}

  register(payload: RegisterPayload): Observable<any> {
    return this.http.post(
      `${this.baseUrl}/register`,
      payload,
      this.publicRequestOptions,
    );
  }

  login(payload: LoginPayload): Observable<any> {
    return this.http.post(
      `${this.baseUrl}/login`,
      payload,
      this.secureRequestOptions,
    );
  }

  confirmEmail(payload: ConfirmEmailPayload): Observable<any> {
    return this.http.post(
      `${this.baseUrl}/confirm-email`,
      payload,
      this.publicRequestOptions,
    );
  }

  forgotPassword(payload: ForgotPasswordPayload): Observable<any> {
    return this.http.post(
      `${this.baseUrl}/forgot-password`,
      payload,
      this.publicRequestOptions,
    );
  }

  resetPassword(payload: ResetPasswordPayload): Observable<any> {
    return this.http.post(
      `${this.baseUrl}/reset-password`,
      payload,
      this.publicRequestOptions,
    );
  }

  logout(): Observable<any> {
    return this.http.post(
      `${this.baseUrl}/logout`,
      {},
      this.secureRequestOptions,
    );
  }

  getStatus(): Observable<any> {
    return this.http.get(`${this.baseUrl}/status`, this.secureRequestOptions);
  }
}
