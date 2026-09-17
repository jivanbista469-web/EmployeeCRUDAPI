
using EmployeeCRUDAPI.Features.Employees;
using EmployeeCRUDAPI.Features.Employees.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCRUDAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            AppSettings.Initialize(builder.Configuration);

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(AppSettings.ConnectionString));
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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
