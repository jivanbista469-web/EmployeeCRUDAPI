
using EmployeeCRUDAPI.Features.Auth.Persistance;
using EmployeeCRUDAPI.Features.Employees;
using EmployeeCRUDAPI.Features.Employees.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCRUDAPI
{
    public class Program
    {
        private const string _corsPolicy = "EmployeeManagementPolicy";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: _corsPolicy,
                                  policy =>
                                  {
                                      policy
                                      .WithOrigins("http://localhost:4200")
                                      .WithHeaders("Content-Type")
                                      .WithMethods("GET", "POST", "PUT", "DELETE");
                                  });
            });

            builder.Services.AddControllers();

            AppSettings.Initialize(builder.Configuration);

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(AppSettings.ConnectionString));
            builder.Services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.User.RequireUniqueEmail = true;
            })
           .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders();
            builder.Services.AddValidatorsFromAssemblyContaining<EmployeeCreateRequestValidator>();
            builder
            .Services
            .Scan(scan => scan
            .FromAssemblyOf<EmployeeService>()
            .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
            .AsSelf()
            .WithScopedLifetime());


            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseCors(_corsPolicy);

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
