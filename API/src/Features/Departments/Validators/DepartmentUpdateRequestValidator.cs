using FluentValidation;

namespace EmployeeCRUDAPI.Features.Departments.Validators
{
    public class DepartmentUpdateRequestValidator : AbstractValidator<DepartmentUpdateRequest>
    {
        public DepartmentUpdateRequestValidator()
        {
            RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.");

            Include(new DepartmentCreateRequestValidator());
        }
    }
}
