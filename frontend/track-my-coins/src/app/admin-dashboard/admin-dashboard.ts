import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { AdminService } from '../services/admin.service';
import { User } from '../shared/models/user.model';
import { RouterModule, Router } from '@angular/router';
import { CommonModule,  } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './admin-dashboard.html',
  styleUrl: './admin-dashboard.css',
})
export class AdminDashboardComponent implements OnInit {
  users: User[] = [];
  selectedUser: any = null;

  constructor(private adminService: AdminService, private router: Router, private cdr: ChangeDetectorRef) {}

  ngOnInit() {
    this.loadUsers();
  }

  loadUsers() {
    this.adminService.getAllUsers().subscribe({
      next: (res) => {
        this.users = [...res];
        this.cdr.detectChanges();  
      },
      error: (err) => console.error("ERROR:", err)
    });
  }

  viewDetails(userId: number) {

    this.router.navigate(['admin/user-details', userId]);
  }

  deleteUser(userId: number) {
    if (confirm('Delete this user?')) {
      this.adminService.deleteUser(userId).subscribe(() => this.loadUsers());
    }
  }
  editUser(user: any) {
    this.selectedUser = { ...user};
  }

  updateUser() {
    if (!this.selectedUser) return;

    this.adminService.editUser(this.selectedUser.id, this.selectedUser)
    .subscribe(() =>{
      alert('Updated!');
      this.selectedUser = null;
      this.loadUsers();
    });
  }
}
