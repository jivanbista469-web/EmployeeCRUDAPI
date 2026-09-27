using EmployeeCRUDAPI.Features.Common;
using EmployeeCRUDAPI.Features.Departments.persistance;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCRUDAPI.Features.Departments
{
    public class DepartmentService
    {
        private readonly ApplicationDbContext _context;

        private readonly IValidator<DepartmentCreateRequest> _departmentCreateRequestValidator;
        private readonly IValidator<DepartmentUpdateRequest> _departmentUpdateRequestValidator;

        public DepartmentService(ApplicationDbContext context, IValidator<DepartmentCreateRequest> departmentCreateRequestValidator,
                                IValidator<DepartmentUpdateRequest> departmentUpdateRequestValidator)
        {
            _context = context;
            _departmentCreateRequestValidator = departmentCreateRequestValidator;
            _departmentUpdateRequestValidator = departmentUpdateRequestValidator;
        }



        #region Read
        public async Task<OutputResponse<List<DepartmentResponse>>> GetAllAsync()
        {
            try
            {
                List<DepartmentResponse> departments = await _context
                                                              .Departments
                                                              .AsNoTracking()
                                                              .Select(e => new DepartmentResponse
                                                              {
                                                                  Id = e.Id,
                                                                  Name = e.Name
                                                              })
                                                              .ToListAsync();
                return OutputResponseConverter.SuccessResponse(departments);
            }
            catch (Exception ex)
            {

                return OutputResponseConverter.FailedResponse<List<DepartmentResponse>>(ex.Message);

            }
        }

        public async Task<OutputResponse<DepartmentResponse>> GetByIdAsync(int id)
        {
            try
            {
                DepartmentResponse department = await _context
                                                      .Departments
                                                      .AsNoTracking()
                                                      .Select(e => new DepartmentResponse
                                                      {
                                                          Id = e.Id,
                                                          Name = e.Name
                                                      })
                                                      .FirstOrDefaultAsync(x => x.Id == id);
                return OutputResponseConverter.SuccessResponse(department);

            }
            catch (Exception ex)
            {

                return OutputResponseConverter.FailedResponse<DepartmentResponse>(ex.Message);

            }
        } 
        #endregion Read

        #region Write
        public async Task<OutputResponse> CreateAsync(DepartmentCreateRequest request)
        {
            try
            {
                ValidationResult validationResult = await _departmentCreateRequestValidator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return OutputResponseConverter.FailedResponse(validationResult);
                }

                Department department = new()
                {
                    Name = request.Name
                };
                await _context.Departments.AddAsync(department);
                await _context.SaveChangesAsync();
                return OutputResponseConverter.SuccessResponse("Department Saved Successfully.");
            }
            catch (Exception ex)
            {
                return OutputResponseConverter.FailedResponse(ex.Message);
            }
        }

        public async Task<OutputResponse> UpdateAsync(DepartmentUpdateRequest request)
        {
            try
            {
                ValidationResult validationResult = await _departmentUpdateRequestValidator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return OutputResponseConverter.FailedResponse(validationResult);
                }
                Department existingDepartment = await _context
                                                      .Departments
                                                      .FirstOrDefaultAsync(e => e.Id == request.Id);
                if (existingDepartment == null)
                {
                    throw new InvalidOperationException("Department not found");
                }

                existingDepartment.Name = request.Name;

                await _context.SaveChangesAsync();
                return OutputResponseConverter.SuccessResponse("Department updated Successfully.");
            }
            catch (Exception ex)
            {
                return OutputResponseConverter.FailedResponse(ex.Message);
            }
        }

        public async Task<OutputResponse> DeleteAsync(int id)
        {
            try
            {
                if (id < 1)
                {
                    return OutputResponseConverter.FailedResponse(["Id is required."]);
                }
                Department existingDepartment = await _context
                                                      .Departments
                                                      .FirstOrDefaultAsync(e => e.Id == id);
                if (existingDepartment == null)
                {
                    throw new InvalidOperationException("Department not found");
                }

                _context.Departments.Remove(existingDepartment);
                await _context.SaveChangesAsync();
                return OutputResponseConverter.SuccessResponse("Department deleted Successfully.");
            }
            catch (Exception ex)
            {
                return OutputResponseConverter.FailedResponse(ex.Message);
            }
        } 
        #endregion Write
    }
}
