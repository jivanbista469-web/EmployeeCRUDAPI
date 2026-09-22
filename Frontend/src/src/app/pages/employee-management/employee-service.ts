import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { EmployeeResponse } from './model/employee-response.model';
import { OutputDataResponse } from '../../model/output-response.model';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Service()
export class EmployeeService {
    private apiUrl = `${environment.apiUrl}/Employee`;
    private http = inject(HttpClient);

    getAll(): Observable<OutputDataResponse<EmployeeResponse[]>> {
        return this.http.get<OutputDataResponse<EmployeeResponse[]>>(this.apiUrl);
    }
}
