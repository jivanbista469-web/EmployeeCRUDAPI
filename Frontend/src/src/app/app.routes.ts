import { Routes } from '@angular/router';
import { EmployeeManagement } from './pages/employee-management/employee-management';
import { DepartmentManagement } from './pages/department-management/department-management';
import { Auth } from './pages/auth/auth';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'employee' },

  { path: 'login', component: Auth },
  
  {
    path: 'employee',
    component: EmployeeManagement,
    canActivate: [authGuard]
  },
  {
    path: 'department',
    component: DepartmentManagement,
    canActivate: [authGuard]
  },

  { path: '**', redirectTo: 'employee' }
];
