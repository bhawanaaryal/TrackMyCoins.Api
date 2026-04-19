import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AdminService } from '../services/admin.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-user-details',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './user-details.html',
  styleUrl: './user-details.css'
})
export class UserDetailsComponent implements OnInit {
  user: any = null;
  expenses: any[] = [];
  budgets: any[] = [];
  categories: any[] = [];
  activeTab: string = 'expenses';
  selectedExpense: any = null;
  selectedBudget: any = null;
  selectedCategory: any = null;

  constructor(
    private route: ActivatedRoute,
    private adminService: AdminService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) this.loadDetails(+id);
  }

  loadDetails(userId: number) {
    this.adminService.getUserDetails(userId).subscribe({
      next: (res) => {
        this.user = res;
        this.expenses = [...res.expenses];
        this.budgets = [...res.budgets];
        this.categories = [...(res as any).categories ?? []];
        this.cdr.detectChanges();
      }
    });
  }


  editExpense(expense: any) { this.selectedExpense = { ...expense }; }
  deleteExpense(id: number) {
    if (confirm('Delete this expense?')) {
      this.adminService.deleteExpense(id).subscribe(() => this.loadDetails(this.user.id));
    }
  }
  updateExpense() {
    if (!this.selectedExpense) return;
    this.adminService.editExpense(this.selectedExpense.id, this.selectedExpense).subscribe(() => {
      this.selectedExpense = null;
      this.loadDetails(this.user.id);
    });
  }

  // Budgets
  editBudget(budget: any) { this.selectedBudget = { ...budget }; }
  deleteBudget(id: number) {
    if (confirm('Delete this budget?')) {
      this.adminService.deleteBudget(id).subscribe(() => this.loadDetails(this.user.id));
    }
  }
  updateBudget() {
    if (!this.selectedBudget) return;
    this.adminService.editBudget(this.selectedBudget.id, this.selectedBudget).subscribe(() => {
      this.selectedBudget = null;
      this.loadDetails(this.user.id);
    });
  }

  goBack() { this.router.navigate(['/admin-dashboard']); }
}