using EmployeeCRUDAPI.Features.Common;
using EmployeeCRUDAPI.Features.Employees;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeCRUDAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        #region Read
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            OutputResponse<List<EmployeeResponse>> response = await _employeeService.GetAllAsync();
            if (response.Suceeded)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            OutputResponse<EmployeeResponse> response = await _employeeService.GetByIdAsync(id);
            if (response.Suceeded)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        #endregion Read

        #region Write

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeCreateRequest request)
        {
            OutputResponse response = await _employeeService.CreateAsync(request);
            if (response.Suceeded)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EmployeeUpdateRequest request)
        {
            OutputResponse response = await _employeeService.UpdateAsync(request);
            if (response.Suceeded)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            OutputResponse response = await _employeeService.DeleteAsync(id);
            if (response.Suceeded)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        #endregion Write
    }
}
