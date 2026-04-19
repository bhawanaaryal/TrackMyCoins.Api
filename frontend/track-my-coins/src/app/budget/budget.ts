
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { BudgetService } from '../services/budget.service';
import { CommonModule } from '@angular/common';
import { Budget } from '../shared/models/budget.model';

@Component({
  selector: 'app-budget',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  styleUrl: './budget.css',
  templateUrl: './budget.html'
})
export class BudgetComponent implements OnInit {

  budgetForm: FormGroup;
  currentBudget: any ;
  months = [
    { name: 'January', value: 1 },
    { name: 'February', value: 2 },
    { name: 'March', value: 3 },
    { name: 'April', value: 4 },
    { name: 'May', value: 5 },
    { name: 'June', value: 6 },
    { name: 'July', value: 7 },
    { name: 'August', value: 8 },
    { name: 'September', value: 9 },
    { name: 'October', value: 10 },
    { name: 'November', value: 11 },
    { name: 'December', value: 12 }
  ];

  constructor(private fb: FormBuilder, private budgetService: BudgetService) {
    const today = new Date();

    this.budgetForm = this.fb.group({
      amount: ['', Validators.required],
      month: [today.getMonth() + 1],
      year: [today.getFullYear()]
    });
  }

  ngOnInit() {
    this.loadBudget();
  }

  loadBudget() {
    const { month, year } = this.budgetForm.value;

    this.budgetService.getBudget(month, year).subscribe({
      next: (res) => {
        this.currentBudget = res;
      }
    });
  }

  onSubmit() {
    if (this.budgetForm.valid) {
      this.budgetService.setBudget(this.budgetForm.value).subscribe({
        next: () => {
          alert('Budget saved!');
          this.loadBudget();
        }
      });
    }
  }
}
