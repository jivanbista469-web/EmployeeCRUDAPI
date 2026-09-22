export interface OutputResponse {
    suceeded: boolean;
    message: string;
    validationResult: any;
    errors: string[];
}

export interface OutputDataResponse<T> extends OutputResponse {
    data: T;
}