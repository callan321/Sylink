import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '@environment/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ProfileService {
  private readonly jsonHeaders = new HttpHeaders({});
  private readonly profileUrl = `${environment.apiUrl}/profile`;

  constructor(private http: HttpClient) {}

  profile(): Observable<any> {
    return this.http.get<Observable<any>>(this.profileUrl, {
      headers: this.jsonHeaders,
      withCredentials: true,
    });
  }
}
