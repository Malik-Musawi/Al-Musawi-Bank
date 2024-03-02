import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loggedInStatus = new BehaviorSubject<boolean>(this.hasToken());
  private currentUserSubject = new BehaviorSubject<any>(this.getUser());

  constructor(private http: HttpClient) {}

  private hasToken(): boolean {
    return !!localStorage.getItem('token');
  }

  private getUser(): any {
    const token = localStorage.getItem('token');
    if (token) {
      try {
        const decoded = jwtDecode(token);
        return decoded;
      } catch (error) {
        console.error('Error decoding token', error);
        return null;
      }
    } else {
      return null;
    }
  }

setToken(token: string): void {
  if (token) {
    localStorage.setItem('token', token);
  } else {
    localStorage.removeItem('token');
  }
}

setUser(user: any): void {
  if (user) {
    localStorage.setItem('user', JSON.stringify(user));
  } else {
    localStorage.removeItem('user');
  }
}


login(email: string, password: string): Observable<any> {
  const loginUrl = 'http://localhost:5240/api/User/login'; // Corrected URL

  return this.http.post(loginUrl, { email, password }).pipe(
    map((response: any) => {
      // Assuming response format is { token: string, userData: any }
      this.setToken(response.token);
      this.setUser(response.userData);
      this.loggedInStatus.next(true);
      this.currentUserSubject.next(response.userData);
      return response;
    }),
    catchError(error => {
      console.error('Login error:', error);
      throw error;
    })
  );
}

  
  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    if (this.currentUserSubject.getValue()) {
      this.currentUserSubject.next(null);
    }
    this.loggedInStatus.next(false);
    this.currentUserSubject.next(null);
  }

  isLoggedIn(): Observable<boolean> {
    return this.loggedInStatus.asObservable();
  }

  getCurrentUser(): Observable<any> {
    return this.currentUserSubject.asObservable();
  }
}