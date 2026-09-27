using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeCRUDAPI.Features.Departments.persistance
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder
            .Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);
        }
    }
}
