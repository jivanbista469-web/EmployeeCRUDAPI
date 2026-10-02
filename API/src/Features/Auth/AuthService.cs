using EmployeeCRUDAPI.Features.Auth.Persistance;
using EmployeeCRUDAPI.Features.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeCRUDAPI.Features.Auth
{
    public class AuthService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<AppUser> _userManager;
        public AuthService(IConfiguration config, UserManager<AppUser> userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        public async Task<OutputResponse<AuthResponse>> LoginAsync(AuthRequest request)
        {
            try
            {
                AppUser user = await _userManager.FindByNameAsync(request.UserName);
                if (user is null)
                {
                    return OutputResponseConverter.FailedResponse<AuthResponse>("Invalid login credential");
                }

                bool isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
                if (!isPasswordValid)
                {
                    return OutputResponseConverter.FailedResponse<AuthResponse>("Invalid login credential");
                }

                string accessToken = GenerateJwtToken(user);
                AuthResponse response = new()
                {
                    UserName = user.UserName,
                    AccessToken = accessToken
                };
                return OutputResponseConverter.SuccessResponse(response);
            }
            catch (Exception ex)
            {
                return OutputResponseConverter.FailedResponse<AuthResponse>(ex.Message);
            }
        }

        public string GenerateJwtToken(AppUser user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(20),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(token);
            string accessToken = tokenHandler.WriteToken(securityToken);
            return accessToken;
        }
    }
}
