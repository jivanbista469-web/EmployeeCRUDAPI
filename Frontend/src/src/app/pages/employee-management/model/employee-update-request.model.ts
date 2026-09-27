import { EmployeeCreateRequest } from "./employee-create-request.model";

export interface EmployeeUpdateRequest extends EmployeeCreateRequest {
    id: number;
}