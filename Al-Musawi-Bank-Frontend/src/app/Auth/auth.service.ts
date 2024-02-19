import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import {jwtDecode} from 'jwt-decode';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loggedInStatus = new BehaviorSubject<boolean>(false);
  private currentUserSubject = new BehaviorSubject<any>(null);
  private tokenKey = 'token';

  constructor(private http: HttpClient) {
    this.initializeAuthState();
  }

  private initializeAuthState(): void {
    const token = this.getTokenFromStorage();
    const tokenIsValid = this.tokenIsValid(token);
    this.loggedInStatus.next(tokenIsValid);
    if (tokenIsValid) {
      const user = this.getCurrentUserFromStorage();
      this.currentUserSubject.next(user);
    } else {
      this.currentUserSubject.next(null);
    }
  }

  private getTokenFromStorage(): string | null {
    return sessionStorage.getItem(this.tokenKey);
  }

  private tokenIsValid(token: string | null): boolean {
    if (!token) {
      return false;
    }
    try {
      const decodedToken = jwtDecode<any>(token);
      return typeof decodedToken.exp !== 'undefined' && decodedToken.exp > Date.now() / 1000;
    } catch (error) {
      return false;
    }
  }

  login(email: string, password: string): Observable<any> {
    const loginUrl = 'http://localhost:5240/login';
    return this.http.post(loginUrl, { email, password });
  }

  handleLoginSuccess(token: string, userData: any): void {
    sessionStorage.setItem(this.tokenKey, token);
    sessionStorage.setItem('user', JSON.stringify(userData));
    this.currentUserSubject.next(userData);
    this.loggedInStatus.next(true);
  }

  logout(): void {
    sessionStorage.removeItem(this.tokenKey);
    sessionStorage.removeItem('user');
    this.currentUserSubject.next(null);
    this.loggedInStatus.next(false);
  }

  isLoggedIn(): Observable<boolean> {
    return this.loggedInStatus.asObservable();
  }

  getCurrentUser(): Observable<any> {
    return this.currentUserSubject.asObservable();
  }

  isAuthenticated(): boolean {
    const token = this.getTokenFromStorage();
    return this.tokenIsValid(token);
  }

  private getCurrentUserFromStorage(): any {
    const userJson = sessionStorage.getItem('user');
    return userJson ? JSON.parse(userJson) : null;
  }
}
