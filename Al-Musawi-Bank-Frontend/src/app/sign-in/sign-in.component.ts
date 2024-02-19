import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../Auth/user.service';
import { AuthService } from '../Auth/auth.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
  loginData = { email: '', password: '' };
  errorMessage: string | null = null;

  constructor(private userService: UserService, private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
  }

  loginUser(): void {
    if (!this.loginData.email || !this.loginData.password) {
      this.errorMessage = 'Please fill in all fields';
      return;
    }

    this.userService.login(this.loginData).subscribe({
      next: (response) => {
        // Assuming the response contains the user data and token
        this.authService.login(response.user, response.token);
        this.router.navigate(['/dashboard']); // Redirect to the dashboard
      },
      error: (error) => {
        console.error('Login failed', error);
        this.errorMessage = 'Login failed. Please check your credentials.';
      }
    });
  }
}