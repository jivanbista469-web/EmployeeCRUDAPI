using FluentValidation.Results;

namespace EmployeeCRUDAPI.Features.Common
{
    public static class OutputResponseConverter
    {
        public static OutputResponse SuccessResponse(string message)
        {
            return new()
            {
                Suceeded = true,
                Message = message
            };
        }

        public static OutputResponse<T> SuccessResponse<T>(T data) where T : class
        {
            return new()
            {
                Suceeded = true,
                Message = ApplicationMessage.Success,
                Data = data
            };
        }

        public static OutputResponse<T> FailedResponse<T>(string error) where T : class
        {
            return new()
            {
                Suceeded = false,
                Message = ApplicationMessage.Failed,
                Errors = [error],
                Data = null
            };
        }

        public static OutputResponse<T> FailedResponse<T>(ValidationResult validationResult) where T : class
        {
            return new()
            {
                Suceeded = false,
                Message = ApplicationMessage.ValidationFailed,
                ValidationResult = validationResult,
                Data = null
            };
        }

        public static OutputResponse FailedResponse(string error)
        {
            return new()
            {
                Suceeded = false,
                Message = ApplicationMessage.Failed,
                Errors = [error]
            };
        }

        public static OutputResponse FailedResponse(ValidationResult validationResult)
        {
            return new()
            {
                Suceeded = false,
                Message = ApplicationMessage.ValidationFailed,
                ValidationResult = validationResult,
            };
        }

        public static OutputResponse FailedResponse(List<string> errors)
        {
            return new()
            {
                Suceeded = false,
                Message = ApplicationMessage.ValidationFailed,
                Errors = errors
            };
        }
    }
}
