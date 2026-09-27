import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { EmployeeResponse } from './model/employee-response.model';
import { OutputDataResponse, OutputResponse } from '../../model/output-response.model';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { EmployeeCreateRequest } from './model/employee-create-request.model';
import { EmployeeUpdateRequest } from './model/employee-update-request.model';

@Service()
export class EmployeeService {
    private apiUrl = `${environment.apiUrl}/Employee`;
    private http = inject(HttpClient);

    getAll(): Observable<OutputDataResponse<EmployeeResponse[]>> {
        return this.http.get<OutputDataResponse<EmployeeResponse[]>>(this.apiUrl);
    }

    create(employee: EmployeeCreateRequest): Observable<OutputResponse> {
        return this.http.post<OutputResponse>(this.apiUrl, employee);
    }

    update(employee: EmployeeUpdateRequest): Observable<OutputResponse> {
        return this.http.put<OutputResponse>(this.apiUrl, employee);
    }

    delete(id: number): Observable<OutputResponse> {
        return this.http.delete<OutputResponse>(`${this.apiUrl}/${id}`);
    }
}
