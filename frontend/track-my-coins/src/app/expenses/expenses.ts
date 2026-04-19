import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormsModule, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ExpenseService } from '../services/expense.service';
import { CommonModule } from '@angular/common';
import { CategoryService } from '../services/category.service';

@Component({
  selector: 'app-expenses',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, FormsModule],
  templateUrl: './expenses.html',
  styleUrl: './expenses.css',
})

export class ExpensesComponent implements OnInit {

  expenseForm: FormGroup;
  expenses: any[] = [];
  categories: any[] = [];
  selectedExpenseId: number | null = null;
  showNewCategory: boolean = false;
  filteredExpenses: any[] = [];

  selectedMonth: number | null = null;
  selectedYear: number | null = null;

  months = [
    { value: 1, name: 'January' },
    { value: 2, name: 'February' },
    { value: 3, name: 'March' },
    { value: 4, name: 'April' },
    { value: 5, name: 'May' },
    { value: 6, name: 'June' },
    { value: 7, name: 'July' },
    { value: 8, name: 'August' },
    { value: 9, name: 'September' },
    { value: 10, name: 'October' },
    { value: 11, name: 'November' },
    { value: 12, name: 'December' }
  ];

  constructor(private fb: FormBuilder, private expenseService: ExpenseService, private categoryService: CategoryService) {
    this.expenseForm = this.fb.group({
      title: ['', Validators.required],
      amount: ['', Validators.required],
      date: ['', Validators.required],
      categoryId: ['', Validators.required]
    });
  }

  ngOnInit() {
    this.loadExpenses();
    this.loadCategories();
  }

  onSubmit() {
    if (this.expenseForm.valid) {

      if (this.selectedExpenseId) {

        this.expenseService.updateExpense(this.selectedExpenseId, this.expenseForm.value)
          .subscribe(() => {
            alert('Updated!');
            this.selectedExpenseId = null;
            this.loadExpenses();
            this.expenseForm.reset();
          });
      } else {

        this.expenseService.addExpense(this.expenseForm.value)
          .subscribe(() => {
            alert('Added!');
            this.loadExpenses();
            this.expenseForm.reset();
          });
      }
    }
  }

  loadExpenses() {
    this.expenseService.getExpenses().subscribe({
      next: (res) => {
        this.expenses = res;

        this.filteredExpenses = [...this.expenses];

        if (this.selectedMonth && this.selectedYear) {
          this.filterExpensesByMonth(this.selectedMonth, this.selectedYear);
        }
      }
    });
  }

  filterExpensesByMonth(month: number, year: number) {
    this.filteredExpenses = this.expenses.filter(exp => {
      const expDate = new Date(exp.date);
      return expDate.getMonth() + 1 === month && expDate.getFullYear() === year;
    });
  }

  onMonthChange(event: Event) {
    const month = (event.target as HTMLSelectElement).value;

    if (!month) {
      this.filteredExpenses = [...this.expenses];
      return;
    }

    const selectedMonth = parseInt(month);
    const year = new Date().getFullYear(); // optional: add year selection later

    this.filteredExpenses = this.expenses.filter(exp => {
      const expDate = new Date(exp.date);
      return expDate.getMonth() + 1 === selectedMonth && expDate.getFullYear() === year;
    });
  }

  loadCategories() {
    this.categoryService.getCategories().subscribe({
      next: (res) => {
        this.categories = res;
      }
    });
  }

  delete(id: number) {
    this.expenseService.deleteExpense(id).subscribe(() => {
      this.loadExpenses();
    });
  }

  edit(expense: any) {
    this.selectedExpenseId = expense.id;

    this.expenseForm.patchValue({
      title: expense.title,
      amount: expense.amount,
      date: expense.date,
      categoryId: expense.categoryId
    });
  }

  newCategory: string = '';

  addCategory() {
    if (!this.newCategory) return;

    this.categoryService.addCategory({ name: this.newCategory })
      .subscribe(() => {
        alert('Category added!');
        this.newCategory = '';
        this.loadCategories(); 
      });
  }

}