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

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private registerUrl = `${environment.apiUrl}/Auth/register`;
  private loginUrl = `${environment.apiUrl}/Auth/login`;

  constructor(private http: HttpClient) {}

  register(user: RegisterPayload): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(this.registerUrl, user, {
      headers,
    });
  }

  login(user: LoginPayload): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(this.loginUrl, user, {
      headers,
      withCredentials: true,
    });
  }
}
