import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../Auth/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  isLoggedIn: boolean = false;
  currentUser: any = null;

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.authService.isLoggedIn().subscribe(loggedInStatus => {
      this.isLoggedIn = loggedInStatus;
    });
  
    this.authService.getCurrentUser().subscribe(user => {
      this.currentUser = user;
    });
  }

  onLogout(): void {
    this.authService.logout();
    this.router.navigate(['/']); // Redirect to home page after logout
  }
}