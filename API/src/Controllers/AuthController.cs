using EmployeeCRUDAPI.Features.Auth;
using EmployeeCRUDAPI.Features.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeCRUDAPI.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthRequest request)
        {
            OutputResponse<AuthResponse> response = await _authService.LoginAsync(request);
            if (response.Suceeded)
            {
                return Ok(response);
            }

            return Unauthorized();
        }
    }
}
