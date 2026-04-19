import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../auth.service';
import { LoginModel } from '../../shared/models/login.model';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';
import {jwtDecode} from 'jwt-decode';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, RouterModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class LoginComponent {

  loginForm: FormGroup;
  testValue = console.log('LoginComponent class loaded');
  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  onSubmit() {
      console.log('Form submitted');
 
    if (this.loginForm.valid) {
      const credentials: LoginModel = this.loginForm.value;

      this.authService.login(credentials).subscribe({
        next: (res) => {
          const decode: any = jwtDecode(res.token);
 
          console.log('login response:', res);
          localStorage.setItem('token', res.token);
          console.log('token saved, about to decode');
          window.dispatchEvent(new Event('loginStateChange'));


const decoded: any = jwtDecode(res.token);

if (decoded['IsAdmin'] === 'True') {
  this.router.navigate(['/admin-dashboard']);
} else {
  this.router.navigate(['/dashboard']);
}
        },
        error: (err) => {
          console.error('Login error:', err);
          
          alert(err.error?.message || 'Login failed');
        }
      });
    }
  }
}