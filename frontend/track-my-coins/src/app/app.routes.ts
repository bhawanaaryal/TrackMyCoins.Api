import { Routes } from '@angular/router';
import {RegisterComponent} from './auth/register/register'
import { LoginComponent } from './auth/login/login';
import { ExpensesComponent } from './expenses/expenses';
import { BudgetComponent } from './budget/budget';
import { DashboardComponent } from './dashboard/dashboard';
import { AuthGuard } from './auth/auth.guard';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard';
import { UserDetailsComponent } from './user-details/user-details';

export const routes: Routes = [
    { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
    {path: 'register', component: RegisterComponent},
    {path: 'login', component: LoginComponent},
    {path: 'expenses', component: ExpensesComponent, canActivate: [AuthGuard]},
    { path: 'budget', component: BudgetComponent, canActivate: [AuthGuard] },
    {path: 'dashboard', component:DashboardComponent, runGuardsAndResolvers: 'always', canActivate: [AuthGuard]},
    {path: 'admin-dashboard', component:AdminDashboardComponent, canActivate: [AuthGuard], data: {adminOnly: true}},
    { path: 'admin/user-details/:id', component: UserDetailsComponent, canActivate: [AuthGuard], data: {adminOnly: true} },
    { path: '**', redirectTo: 'login' },

];
