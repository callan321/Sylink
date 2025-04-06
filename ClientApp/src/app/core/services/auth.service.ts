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
  private readonly jsonHeaders = new HttpHeaders({
    'Content-Type': 'application/json',
  });

  private readonly registerUrl = `${environment.apiUrl}/Auth/register`;
  private readonly loginUrl = `${environment.apiUrl}/Auth/login`;
  private readonly confirmEmailUrl = `${environment.apiUrl}/Auth/confirm-email`;
  private readonly forgotPasswordUrl = `${environment.apiUrl}/Auth/forgot-password`;
  private readonly resetPasswordUrl = `${environment.apiUrl}/Auth/reset-password`;
  private readonly logoutUrl = `${environment.apiUrl}/Auth/logout`;
  private readonly refreshUrl = `${environment.apiUrl}/Auth/refresh-token`;

  constructor(private http: HttpClient) {}

  register(payload: RegisterPayload): Observable<any> {
    return this.http.post(this.registerUrl, payload, {
      headers: this.jsonHeaders,
    });
  }

  login(payload: LoginPayload): Observable<any> {
    return this.http.post(this.loginUrl, payload, {
      headers: this.jsonHeaders,
      withCredentials: true,
    });
  }

  confirmEmail(payload: ConfirmEmailPayload): Observable<any> {
    return this.http.post(this.confirmEmailUrl, payload, {
      headers: this.jsonHeaders,
    });
  }

  forgotPassword(payload: ForgotPasswordPayload): Observable<any> {
    return this.http.post(this.forgotPasswordUrl, payload, {
      headers: this.jsonHeaders,
    });
  }

  resetPassword(payload: ResetPasswordPayload): Observable<any> {
    return this.http.post(this.resetPasswordUrl, payload, {
      headers: this.jsonHeaders,
    });
  }

  logout(): Observable<any> {
    return this.http.post(
      this.logoutUrl,
      {},
      {
        headers: this.jsonHeaders,
        withCredentials: true,
      },
    );
  }

  refresh(): Observable<any> {
    return this.http.post(
      this.refreshUrl,
      {},
      {
        headers: this.jsonHeaders,
        withCredentials: true,
      },
    );
  }
}
