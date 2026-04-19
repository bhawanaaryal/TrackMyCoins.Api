import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RegisterModel } from '../shared/models/register.model';
import { LoginModel } from '../shared/models/login.model';


@Injectable({
  providedIn: 'root',
})

export class AuthService {

  private baseURL = 'http://localhost:5145/api/users';
  constructor(private http: HttpClient) {}

  register(user: RegisterModel): Observable<any> {
    return this.http.post(`${this.baseURL}/register`, user);
  }

  login(credentials: LoginModel): Observable<any> {
    return this.http.post(`${this.baseURL}/login`, credentials);
}

  
  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }


  getToken(): string | null {
    return localStorage.getItem('token');
  }

  logout(): void {
    localStorage.removeItem('token');
}

}
