import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BudgetService {

  private baseURL = 'http://localhost:5145/api/budgets';

  constructor(private http: HttpClient) {}

  setBudget(data: any): Observable<any> {
    return this.http.post(this.baseURL, data);
  }

  getBudget(month: number, year: number): Observable<any> {
    return this.http.get(`${this.baseURL}?month=${month}&year=${year}`);
  }
}