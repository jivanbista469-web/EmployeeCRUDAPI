import { Routes } from '@angular/router';
import { EmployeeManagement } from './pages/employee-management/employee-management';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'employee' },

  {
    path: 'employee',
    component: EmployeeManagement,
  },

  { path: '**', redirectTo: 'employee' }
];
