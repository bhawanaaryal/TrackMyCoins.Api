import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  private baseURL = 'http://localhost:5145/api/dashboard';

  constructor(private http: HttpClient) {}

  getSummary(month: number, year: number): Observable<any> {
    return this.http.get(`${this.baseURL}/summary?month=${month}&year=${year}`);
  }
}