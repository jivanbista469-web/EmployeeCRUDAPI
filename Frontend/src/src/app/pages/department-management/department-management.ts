import { Component, inject, signal } from '@angular/core';
import { DepartmentResponse } from './model/department-response.model';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { DepartmentService } from './department-service';
import { DepartmentUpdateRequest } from './model/department-update-request.model';

@Component({
  imports: [ReactiveFormsModule],
  selector: 'app-department-management',
  styleUrl: './department-management.scss',
  templateUrl: './department-management.html',
})
export class DepartmentManagement {
  title = signal<string>('Department Management');
  departments = signal<DepartmentResponse[]>([]);
  currentDepartmentId = signal<number>(0);

  private fb = inject(FormBuilder);

  departmentForm: FormGroup = this.fb.group({
    id: [0],
    name: ['', [Validators.required, Validators.maxLength(100)]]
  });

  constructor(private departmentService: DepartmentService) { }

  ngOnInit(): void {
    this.loadDepartments();
  }

  loadDepartments(): void {
    this.departmentService.getAll().subscribe(response => {
      if (response.suceeded) {
        this.departments.set(response.data);
      }
    });
  }

  upsertDepartment(): void {
    if (this.departmentForm.invalid) {
      this.departmentForm.markAllAsTouched();
      return;
    }

    const departmentData: DepartmentUpdateRequest = this.departmentForm.value;

    if (this.currentDepartmentId() > 0) {
      this.departmentService.update(departmentData).subscribe({
        next: () => {
          this.onSuccess();
        },
        error: (err) => console.error('Error updating department:', err)
      });
    } else {
      this.departmentService.create(departmentData).subscribe({
        next: () => {
          this.onSuccess();
        },
        error: (err) => console.error('Error creating department:', err)
      });
    }
  }

  private onSuccess() {
    this.loadDepartments();
    this.resetForm();
  }

  editDepartment(employee: DepartmentUpdateRequest): void {
    this.currentDepartmentId.set(employee.id);
    this.departmentForm.patchValue({
      id: employee.id,
      name: employee.name
    });
  }

  deleteDepartment(id: number): void {
    if (id > 0 && confirm('Are you sure you want to delete this department ?')) {
      this.departmentService.delete(id).subscribe({
        next: () => {
          this.onSuccess();
        },
        error: (err) => console.error('Error deleting department:', err)
      });;
    }
  }

  resetForm(): void {
    this.departmentForm.reset();
    this.currentDepartmentId.set(0);
  }
}
