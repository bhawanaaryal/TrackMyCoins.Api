import { Component } from '@angular/core';
import { FormBuilder, Validators, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../auth.service';
import { RegisterModel } from '../../shared/models/register.model';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, RouterModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})

export class RegisterComponent {
  registerForm: FormGroup;
  constructor (private fb: FormBuilder, private authService: AuthService) {
      this.registerForm = this.fb.group({
          name: ['', Validators.required],
          email: ['', [Validators.required, Validators.email]],
          password: ['', Validators.required]
});
  }

  onSubmit() {
    console.log(this.registerForm.value);
    if (this.registerForm.valid) {
      const user: RegisterModel = this.registerForm.value;
      this.authService.register(user).subscribe({
        next: (res) => {
          console.log('Registered:', res);
          alert('Registration successful!!');
        },
        error: (err) => {
        console.error('Registration error:', err);
        alert(err.error?.message || 'Registration failed');
        }
      });
    }
  }
}
