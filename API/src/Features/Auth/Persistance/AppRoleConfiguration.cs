using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace EmployeeCRUDAPI.Features.Auth.Persistance
{
    public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder
            .Property(u => u.CreatedDate)
            .HasDefaultValueSql("GETDATE()");
        }
    }
}

