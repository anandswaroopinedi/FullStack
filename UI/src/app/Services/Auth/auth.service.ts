import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http:HttpClient) {}

  setToken(token: string): void {
    sessionStorage.setItem('token', token.replace(/['"]+/g ,''));
  }

  getToken(): string | null {
    return sessionStorage.getItem('token');
  }

  removeToken(): void {
    sessionStorage.removeItem('token');
  }
  validateToken(token:string):Observable<{valid: boolean}>
  {
    const params = new HttpParams()
      .set('token', token)
    return this.http.get<{valid: boolean}>('https://localhost:7270/validate-token',{params});
  }
}
