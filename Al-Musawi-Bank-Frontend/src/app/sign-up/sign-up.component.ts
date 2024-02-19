import { Component } from '@angular/core';
import { UserService } from '../Auth/user.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent {
  signUpData = {
    name: '',
    email: '',
    password: ''
  };
  errorMessage: string | null = null;

  constructor(private userService: UserService) {}

  registerUser() {
    this.userService.register(this.signUpData).subscribe(
      response => {
        console.log('User registered:', response);
        this.errorMessage = null;
        // Navigate to login or dashboard
         
      },
      error => {
        console.error('Registration error:', error);
        this.errorMessage = 'Registration failed. Please try again.';
        // Display error message
      }
    );
  }
}
