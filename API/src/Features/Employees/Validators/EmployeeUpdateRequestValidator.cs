using FluentValidation;

namespace EmployeeCRUDAPI.Features.Employees.Validators
{
    public class EmployeeUpdateRequestValidator : AbstractValidator<EmployeeUpdateRequest>
    {
        public EmployeeUpdateRequestValidator()
        {
            RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.");

            Include(new EmployeeCreateRequestValidator());
        }
    }
}
