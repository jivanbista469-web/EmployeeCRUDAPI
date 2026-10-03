
using EmployeeCRUDAPI.Features.Auth.Persistance;
using EmployeeCRUDAPI.Features.Employees;
using EmployeeCRUDAPI.Features.Employees.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            // Read JWT settings from appsettings.json
            string jwtKey = builder.Configuration["Jwt:Key"];
            string jwtIssuer = builder.Configuration["Jwt:Issuer"];
            string jwtAudience = builder.Configuration["Jwt:Audience"];

            // Add Authentication Services
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                    ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"Auth failed: {context.Exception.Message}");
                        return Task.CompletedTask;
                    }
                };
            });

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

            app.UseAuthentication();
            
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
