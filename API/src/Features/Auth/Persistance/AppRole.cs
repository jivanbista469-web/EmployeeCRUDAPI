using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeCRUDAPI.Features.Auth.Persistance
{
    public class AppRole : IdentityRole<int>
    {
        public DateTime? CreatedDate { get; set; }
    }
}
