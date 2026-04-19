import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  constructor(private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const token = localStorage.getItem('token');
    console.log('GUARD RUNNING, token:', token);
    
    if (!token) {
      alert('Please login first!');
      this.router.navigate(['/login']);
      return false;
    }

    if (route.data?.['adminOnly']) {
      const decoded: any = jwtDecode(token);
      console.log('DECODED:', decoded);
      console.log('IsAdmin value:', decoded['IsAdmin']);
      console.log('IsAdmin check:', decoded['IsAdmin'] !== 'True');
      if (decoded['IsAdmin'] !== 'True') {
        alert('You are not allowed to access this page!');
        this.router.navigate(['/dashboard']);
        return false;
      }
    }

    return true;
  }

}
