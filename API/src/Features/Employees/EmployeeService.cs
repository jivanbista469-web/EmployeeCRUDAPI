using Azure;
using EmployeeCRUDAPI.Features.Common;
using EmployeeCRUDAPI.Features.Employees.persistance;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCRUDAPI.Features.Employees
{
    public class EmployeeService
    {
        private readonly ApplicationDbContext _context;

        private readonly IValidator<EmployeeCreateRequest> _employeeCreateRequestValidator;
        private readonly IValidator<EmployeeUpdateRequest> _employeeUpdateRequestValidator;

        public EmployeeService(ApplicationDbContext context, IValidator<EmployeeCreateRequest> employeeCreateRequestValidator,
                               IValidator<EmployeeUpdateRequest> employeeUpdateRequestValidator)
        {
            _context = context;
            _employeeCreateRequestValidator = employeeCreateRequestValidator;
            _employeeUpdateRequestValidator = employeeUpdateRequestValidator;
        }

        #region Read
        public async Task<OutputResponse<List<EmployeeResponse>>> GetAllAsync()
        {
            try
            {
                List<EmployeeResponse> employees = await _context
                                                              .Employees
                                                              .AsNoTracking()
                                                              .Select(e => new EmployeeResponse
                                                              {
                                                                  Id = e.Id,
                                                                  Name = e.Name,
                                                                  Salary = e.Salary,
                                                                  Address = e.Address
                                                              })
                                                              .ToListAsync();
                return OutputResponseConverter.SuccessResponse(employees);
            }
            catch (Exception ex)
            {

                return OutputResponseConverter.FailedResponse<List<EmployeeResponse>>(ex.Message);

            }
        }

        public async Task<OutputResponse<EmployeeResponse>> GetByIdAsync(int id)
        {
            try
            {
                EmployeeResponse employee = await _context
                                                      .Employees
                                                      .AsNoTracking()
                                                      .Select(e => new EmployeeResponse
                                                      {
                                                          Id = e.Id,
                                                          Name = e.Name,
                                                          Salary = e.Salary,
                                                          Address = e.Address
                                                      })
                                                      .FirstOrDefaultAsync(x => x.Id == id);
                return OutputResponseConverter.SuccessResponse(employee);

            }
            catch (Exception ex)
            {

                return OutputResponseConverter.FailedResponse<EmployeeResponse>(ex.Message);

            }
        } 
        #endregion Read

        #region Write
        public async Task<OutputResponse> CreateAsync(EmployeeCreateRequest request)
        {
            try
            {
                ValidationResult validationResult = await _employeeCreateRequestValidator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return OutputResponseConverter.FailedResponse(validationResult);
                }

                Employee employee = new()
                {
                    Name = request.Name,
                    Salary = request.Salary,
                    Address = request.Address
                };
                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();
                return OutputResponseConverter.SuccessResponse("Employee Saved Successfully.");
            }
            catch (Exception ex)
            {
                return OutputResponseConverter.FailedResponse(ex.Message);
            }
        }

        public async Task<OutputResponse> UpdateAsync(EmployeeUpdateRequest request)
        {
            try
            {
                ValidationResult validationResult = await _employeeCreateRequestValidator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return OutputResponseConverter.FailedResponse(validationResult);
                }
                Employee existingEmployee = await _context
                                                      .Employees
                                                      .FirstOrDefaultAsync(e => e.Id == request.Id);
                if (existingEmployee == null)
                {
                    throw new InvalidOperationException("Employee not found");
                }

                existingEmployee.Name = request.Name;
                existingEmployee.Salary = request.Salary;
                existingEmployee.Address = request.Address;

                await _context.SaveChangesAsync();
                return OutputResponseConverter.SuccessResponse("Employee updated Successfully.");
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
                Employee existingEmployee = await _context
                                                      .Employees
                                                      .FirstOrDefaultAsync(e => e.Id == id);
                if (existingEmployee == null)
                {
                    throw new InvalidOperationException("Employee not found");
                }

                _context.Employees.Remove(existingEmployee);
                await _context.SaveChangesAsync();
                return OutputResponseConverter.SuccessResponse("Employee deleted Successfully.");
            }
            catch (Exception ex)
            {
                return OutputResponseConverter.FailedResponse(ex.Message);
            }
        } 
        #endregion Write
    }
}
