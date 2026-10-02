import { Routes } from '@angular/router';
import { EmployeeManagement } from './pages/employee-management/employee-management';
import { DepartmentManagement } from './pages/department-management/department-management';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'employee' },
  
  {
    path: 'employee',
    component: EmployeeManagement,
  },
  {
    path: 'department',
    component: DepartmentManagement,
  },

  { path: '**', redirectTo: 'employee' }
];
