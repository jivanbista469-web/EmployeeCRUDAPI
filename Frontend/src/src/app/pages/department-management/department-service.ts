import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { OutputDataResponse, OutputResponse } from '../../model/output-response.model';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { DepartmentResponse } from './model/department-response.model';
import { DepartmentCreateRequest } from './model/department-create-request.model';
import { DepartmentUpdateRequest } from './model/department-update-request.model';

@Service()
export class DepartmentService {
    private apiUrl = `${environment.apiUrl}/Department`;
    private http = inject(HttpClient);

    getAll(): Observable<OutputDataResponse<DepartmentResponse[]>> {
        return this.http.get<OutputDataResponse<DepartmentResponse[]>>(this.apiUrl);
    }

    create(department: DepartmentCreateRequest): Observable<OutputResponse> {
        return this.http.post<OutputResponse>(this.apiUrl, department);
    }

    update(department: DepartmentUpdateRequest): Observable<OutputResponse> {
        return this.http.put<OutputResponse>(this.apiUrl, department);
    }

    delete(id: number): Observable<OutputResponse> {
        return this.http.delete<OutputResponse>(`${this.apiUrl}/${id}`);
    }
}
