import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { tap } from 'rxjs/operators';


const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  
@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly API_URL = 'http://localhost:5234/api/identity'; // replace with your API URL

  constructor(private http: HttpClient) {}

  

  login(username: string, password: string) {
    return this.http.post<any>(`${this.API_URL}/Login`, { username, password },httpOptions).pipe(
      tap(res => {
        window.sessionStorage.setItem('token', res.jwtToken);
        window.sessionStorage.setItem('role', res.role);
      })
    );
  }

  register(username: string, password: string, email: string) {
    return this.http.post<any>(`${this.API_URL}/Register`, { username, password, email },httpOptions).pipe();
  }

  logout() {
    window.sessionStorage.clear();
  }

  isLoggedIn() {
    return !!window.sessionStorage.getItem('token');
  }

  getToken() {
    return window.sessionStorage.getItem('token');
  }

  getRole() {
    return window.sessionStorage.getItem('role');
  }

  isManager() {
    return  this.getRole() == 'Manager';
  }
}