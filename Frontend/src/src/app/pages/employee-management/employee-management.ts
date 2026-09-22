import { Component } from '@angular/core';
import { EmployeeService } from './employee-service';
import { EmployeeResponse } from './model/employee-response.model';
 
@Component({
  imports: [],
  selector: 'app-employee-management',
  styleUrl: './employee-management.scss',
  templateUrl: './employee-management.html',
})
export class EmployeeManagement {
  title: string = 'Employee Management';
  employees: EmployeeResponse[] = [];

  constructor(private employeeService: EmployeeService) { }

  ngOnInit(): void {
    this.loadEmployees();
  }

  loadEmployees(): void {
    this.employeeService.getAll().subscribe( response => {
      if(response.suceeded) {
        this.employees = response.data;
      }
    });
  }
}
