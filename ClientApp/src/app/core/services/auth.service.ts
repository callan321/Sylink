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
  private registerUrl = `${environment.apiUrl}/Auth/register`;
  private loginUrl = `${environment.apiUrl}/Auth/login`;
  private confirmEmailUrl = `${environment.apiUrl}/Auth/verify-email`;
  private forgotPasswordUrl = `${environment.apiUrl}/Auth/forgot-password`;
  private resetPasswordUrl = `${environment.apiUrl}/Auth/reset-password`;

  constructor(private http: HttpClient) {}

  register(payload: RegisterPayload): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(this.registerUrl, payload, {
      headers,
    });
  }

  login(payload: LoginPayload): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(this.loginUrl, payload, {
      headers,
      withCredentials: true,
    });
  }

  confirmEmail(payload: ConfirmEmailPayload): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(this.confirmEmailUrl, payload, { headers });
  }

  forgotPassword(payload: ForgotPasswordPayload): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(this.forgotPasswordUrl, payload, {
      headers,
    });
  }

  resetPassword(payload: ResetPasswordPayload): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(this.resetPasswordUrl, payload, {
      headers,
    });
  }
}
