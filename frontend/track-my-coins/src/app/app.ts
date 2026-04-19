import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthService } from './auth/auth.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-root',
  imports: [RouterModule, CommonModule],
  standalone: true,
  templateUrl: './app.html',
  styleUrl: './app.css'
})

export class App {
  protected readonly title = signal('track-my-coins');
  isLoggedIn: boolean = false;

  constructor(private authService: AuthService, private router: Router) {
    this.updateLoginState();

  window.addEventListener('loginStateChange', () => {
      this.updateLoginState();
    });
  }

  updateLoginState() {
    this.isLoggedIn = this.authService.isLoggedIn();
  }

  logout() {
    this.authService.logout();
    this.updateLoginState();
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
 
}
