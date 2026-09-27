import { Component, inject, signal } from '@angular/core';
import { EmployeeService } from './employee-service';
import { EmployeeResponse } from './model/employee-response.model';
import { EmployeeUpdateRequest } from './model/employee-update-request.model';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
// import { DepartmentService } from '../department-management/department-service';
// import { DepartmentResponse } from '../department-management/model/department-response.model';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-employee-management',
  standalone: true,
  imports: [ReactiveFormsModule, NgbDropdownModule],
  templateUrl: './employee-management.html',
  styleUrl: './employee-management.scss',
})
export class EmployeeManagement {
  title = signal<string>('Employee Management');
  employees = signal<EmployeeResponse[]>([]);
  currentEmployeeId = signal<number>(0);
  // departments = signal<DepartmentResponse[]>([]);
  // selectedDepartment = signal<string | null>(null);

  private fb = inject(FormBuilder);

  employeeForm: FormGroup = this.fb.group({
    id: [0],
    name: ['', [Validators.required, Validators.maxLength(100)]],
    salary: ['', [Validators.required]],
    // departmentId: [null, Validators.required],
    address: ['']
  });

  constructor(private employeeService: EmployeeService) { }

  ngOnInit(): void {
    this.loadEmployees();
    // this.loadDepartments();
  }

  loadEmployees(): void {
    this.employeeService.getAll().subscribe(response => {
      if (response.suceeded) {
        this.employees.set(response.data);
      }
    });
  }

  // loadDepartments(): void {
  //   this.departmentService.getAll().subscribe(response => {
  //     if (response.suceeded) {
  //       this.departments.set(response.data);
  //     }
  //   });
  // }

  // onSelect(dept: DepartmentResponse | null): void {
  //   if (dept) {
  //     this.selectedDepartment.set(dept.name);
  //   } else {
  //     this.selectedDepartment.set(null);
  //   }
  //   this.employeeForm.get('departmentId')?.setValue(dept ? dept.id : null);
  //   this.employeeForm.get('departmentId')?.markAsTouched();
  // }

  upsertEmployee(): void {
    if (this.employeeForm.invalid) {
      this.employeeForm.markAllAsTouched();
      return;
    }

    const employeeData: EmployeeUpdateRequest = this.employeeForm.value;

    if (this.currentEmployeeId() > 0) {
      this.employeeService.update(employeeData).subscribe({
        next: () => {
          this.onSuccess();
        },
        error: (err) => console.error('Error updating employee:', err)
      });
    } else {
      this.employeeService.create(employeeData).subscribe({
        next: () => {
          this.onSuccess();
        },
        error: (err) => console.error('Error creating employee:', err)
      });
    }
  }

  private onSuccess() {
    this.loadEmployees();
    this.resetForm();
  }

  editEmployee(employee: EmployeeResponse): void {
    this.currentEmployeeId.set(employee.id);
    this.employeeForm.patchValue({
      id: employee.id,
      name: employee.name,
      salary: employee.salary,
      // departmentId: employee.departmentId,
      address: employee.address
    });
    // const matchedDept = this.departments().find(d => d.id === employee.departmentId);
    // this.selectedDepartment.set(matchedDept ? matchedDept.name : null);
  }

  deleteEmployee(id: number): void {
    if (id > 0 && confirm('Are you sure you want to delete this employee ?')) {
      this.employeeService.delete(id).subscribe({
        next: () => {
          this.onSuccess();
        },
        error: (err) => console.error('Error deleting employee:', err)
      });;
    }
  }

  resetForm(): void {
    // this.employeeForm.get('departmentId')?.setValue(0);
    // this.selectedDepartment.set(null);

    this.employeeForm.reset();
    this.currentEmployeeId.set(0);
  }
}
