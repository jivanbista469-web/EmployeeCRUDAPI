using FluentValidation.Results;

namespace EmployeeCRUDAPI.Features.Common
{
    public class OutputResponse
    {
        public bool Suceeded { get; set; }
        public string Message { get; set; }
        public ValidationResult ValidationResult { get; set; }
        public List<string> Errors { get; set; }

        public OutputResponse()
        {
            Errors = [];
        }
    }

    public class OutputResponse<T> : OutputResponse where T : class
    {
        public T Data { get; set; }
    }
}