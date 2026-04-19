import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class ExpenseService {

  private baseURL = 'http://localhost:5145/api/expenses';

  constructor(private http: HttpClient) {}

  addExpense(expense: any): Observable<any> {
    return this.http.post(this.baseURL, expense);
  }

  getExpenses(): Observable<any> {
    return this.http.get(this.baseURL);
  }

  deleteExpense(id: number) {
  return this.http.delete(`${this.baseURL}/${id}`);
  
}

updateExpense(id: number, expense: any) {
  return this.http.put(`${this.baseURL}/${id}`, expense);
}
  
}