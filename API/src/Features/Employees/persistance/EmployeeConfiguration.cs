using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeCRUDAPI.Features.Employees.persistance
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder
            .Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

            builder
            .Property(e => e.Address)
            .HasMaxLength(200);
        }
    }
}
