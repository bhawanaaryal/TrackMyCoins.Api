import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../shared/models/user.model';
import { Expense } from '../shared/models/expense.model';
import { Budget } from '../shared/models/budget.model';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private baseUrl = 'http://localhost:5145/api/users';

  constructor(private http: HttpClient) {}

  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.baseUrl}/all-users`);
  }

  getUserDetails(userId: number): Observable<{expenses: Expense[], budgets: Budget[], id:number, name:string, email:string}> {
    return this.http.get<any>(`${this.baseUrl}/${userId}/details`);
  }

  editUser(userId: number, data: User) {
    return this.http.put(`${this.baseUrl}/${userId}`, data);
  }

  deleteUser(userId: number) {
    return this.http.delete(`${this.baseUrl}/${userId}`);
  }

  editExpense(expenseId: number, data: Expense) {
    return this.http.put(`${this.baseUrl}/expenses/${expenseId}`, data);
  }

  deleteExpense(expenseId: number) {
    return this.http.delete(`${this.baseUrl}/expenses/${expenseId}`);
  }

  editBudget(budgetId: number, data: Budget) {
    return this.http.put(`${this.baseUrl}/budgets/${budgetId}`, data);
  }

  deleteBudget(budgetId: number) {
    return this.http.delete(`${this.baseUrl}/budgets/${budgetId}`);
  }
}