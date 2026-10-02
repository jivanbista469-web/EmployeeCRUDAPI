using Microsoft.AspNetCore.Identity;

namespace EmployeeCRUDAPI.Features.Auth.Persistance
{
    public class AppUser : IdentityUser<int>
    {
        public DateTime? CreatedDate { get; set; }
    }
}
