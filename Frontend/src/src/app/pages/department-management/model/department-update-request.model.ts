import { DepartmentCreateRequest } from "./department-create-request.model";

export interface DepartmentUpdateRequest extends DepartmentCreateRequest {
    id: number;
}