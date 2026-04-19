import { Component, OnInit } from '@angular/core';
import { DashboardService } from '../services/dashboard.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { NavigationEnd } from '@angular/router';
import { filter } from 'rxjs';


@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.html',
    styleUrl: './dashboard.css',

})
export class DashboardComponent implements OnInit {

  data: any;
  isOverBudget: boolean = false;
  alertTimeout: any;

  constructor(private dashboardService: DashboardService, private router: Router) {}

  ngOnInit() {
  this.loadData();

  this.router.events
    .pipe(filter(event => event instanceof NavigationEnd))
    .subscribe(() => {
      this.loadData();
    });
}
  loadData() {
  const today = new Date();
  const month = today.getMonth() + 1;
  const year = today.getFullYear();

  this.dashboardService.getSummary(month, year).subscribe({
      next: res => {
        console.log('Dashboard API Response:', res);
        this.data = res;
       this.isOverBudget = res.totalSpent > res.budget;


  if (this.isOverBudget) {
    if (this.alertTimeout) {
      clearTimeout(this.alertTimeout);
    }
    this.alertTimeout = setTimeout(() => {
      this.isOverBudget = false;
    }, 3000);
  }
},
      error: err => console.error(err)
    });
  }

  getBarWidth(amount: number, max: number = this.data?.totalSpent || 100): number {
    return Math.min((amount / max) * 100, 100);
  }

  
}

