using EmployeeCRUDAPI.Features.Common;
using EmployeeCRUDAPI.Features.Departments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeCRUDAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentService _departmentService;

        public DepartmentController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        #region Read
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            OutputResponse<List<DepartmentResponse>> response = await _departmentService.GetAllAsync();
            if (response.Suceeded)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            OutputResponse<DepartmentResponse> response = await _departmentService.GetByIdAsync(id);
            if (response.Suceeded)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        #endregion Read

        #region Write

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DepartmentCreateRequest request)
        {
            OutputResponse response = await _departmentService.CreateAsync(request);
            if (response.Suceeded)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] DepartmentUpdateRequest request)
        {
            OutputResponse response = await _departmentService.UpdateAsync(request);
            if (response.Suceeded)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            OutputResponse response = await _departmentService.DeleteAsync(id);
            if (response.Suceeded)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        #endregion Write
    }
}
