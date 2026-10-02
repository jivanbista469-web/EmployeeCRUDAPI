using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeCRUDAPI.Features.Auth.Persistance
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder
            .HasIndex(x => x.UserName)
            .IsUnique();

            builder
            .Property(u => u.UserName)
            .IsRequired();

            builder
            .Property(u => u.PasswordHash)
            .IsRequired();

            builder
            .Property(u => u.CreatedDate)
            .HasDefaultValueSql("GETDATE()");

            builder.HasData(GetDefaultUsers());
        }

        public List<AppUser> GetDefaultUsers()
        {
            string adminUserName = "jivan";
            string adminEmail = "jivan@example.com";
            string managerUserName = "manager";
            string managerEmail = "manager@example.com";
            string inventoryUserName = "inventory";
            string inventoryEmail = "inventory@example.com";
            string salesUserName = "sales";
            string salesEmail = "sales@example.com";
            return new()
            {
                new AppUser
                {
                    Id = 1,
                    EmailConfirmed = true,
                    UserName = adminUserName,
                    NormalizedUserName = adminUserName.ToUpper(),
                    Email = adminEmail,
                    NormalizedEmail = adminEmail.ToUpper(),
                    ConcurrencyStamp = "bcad4ecc-d1b5-454b-9ddf-1f63a917e17c",
                    SecurityStamp = "e489d3da-3200-4ce4-b408-0a48f0f2484a",
                    //PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "Jivan1@3"),
                    PasswordHash = "AQAAAAIAAYagAAAAEO0Oo+Q3icvsVfRc8JDMcwRxqvxK6NjjacqbD4+ixp3Ot6A3lggJo6zZTX6MuDXm/w==",
                    CreatedDate = Convert.ToDateTime("2026-07-24 01:47:54.7766667")
                },
                new AppUser
                {
                    Id = 2,
                    EmailConfirmed = true,
                    UserName = managerUserName,
                    NormalizedUserName = managerUserName.ToUpper(),
                    Email = managerEmail,
                    NormalizedEmail = managerEmail.ToUpper(),
                    ConcurrencyStamp = "f05e6c47-f022-496f-8d00-b8bf2ea6682d",
                    SecurityStamp = "88868609-5356-4ffc-bcec-600286edca81",
                    //PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "Jivan1@3"),
                    PasswordHash = "AQAAAAIAAYagAAAAEO0Oo+Q3icvsVfRc8JDMcwRxqvxK6NjjacqbD4+ixp3Ot6A3lggJo6zZTX6MuDXm/w==",
                    CreatedDate = Convert.ToDateTime("2026-07-24 01:47:54.7766667")
                },
                new AppUser
                {
                    Id = 3,
                    EmailConfirmed = true,
                    UserName = inventoryUserName,
                    NormalizedUserName = inventoryUserName.ToUpper(),
                    Email = inventoryEmail,
                    NormalizedEmail = inventoryEmail.ToUpper(),
                    ConcurrencyStamp = "8a8644f9-9b0a-450e-a14c-3b1e661d84b2",
                    SecurityStamp = "a66a2f00-985f-4425-8cf2-e9ca5d06b3e9",
                    //PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "Jivan1@3"),
                    PasswordHash = "AQAAAAIAAYagAAAAEO0Oo+Q3icvsVfRc8JDMcwRxqvxK6NjjacqbD4+ixp3Ot6A3lggJo6zZTX6MuDXm/w==",
                    CreatedDate = Convert.ToDateTime("2026-07-24 01:47:54.7766667")
                },
                new AppUser
                {
                    Id = 4,
                    EmailConfirmed = true,
                    UserName = salesUserName,
                    NormalizedUserName = salesUserName.ToUpper(),
                    Email = salesEmail,
                    NormalizedEmail = salesEmail.ToUpper(),
                    ConcurrencyStamp = "130e1e71-79c6-4f52-83c7-82f08669e840",
                    SecurityStamp = "d601c71f-0303-4ed6-8fee-80c387b1197f",
                    //PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "Jivan1@3"),
                    PasswordHash = "AQAAAAIAAYagAAAAEO0Oo+Q3icvsVfRc8JDMcwRxqvxK6NjjacqbD4+ixp3Ot6A3lggJo6zZTX6MuDXm/w==",
                    CreatedDate = Convert.ToDateTime("2026-07-24 01:47:54.7766667")
                }
            };

        }
    }
}

