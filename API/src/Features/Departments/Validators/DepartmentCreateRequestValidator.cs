using FluentValidation;

namespace EmployeeCRUDAPI.Features.Departments.Validators
{
    public class DepartmentCreateRequestValidator : AbstractValidator<DepartmentCreateRequest>
    {
        public DepartmentCreateRequestValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(100)
            .WithMessage("Name cannot exceed 100 characters.");
        }
    }
}
