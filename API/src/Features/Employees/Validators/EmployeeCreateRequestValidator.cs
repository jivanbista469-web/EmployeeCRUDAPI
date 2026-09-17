using FluentValidation;

namespace EmployeeCRUDAPI.Features.Employees.Validators
{
    public class EmployeeCreateRequestValidator : AbstractValidator<EmployeeCreateRequest>
    {
        public EmployeeCreateRequestValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(100)
            .WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Salary)
            .GreaterThan(0)
            .WithMessage("Salary must be greater than 0.");

            RuleFor(x => x.Address)
            .MaximumLength(200)
            .WithMessage("Address cannot exceed 200 characters.");
        }
    }
}
