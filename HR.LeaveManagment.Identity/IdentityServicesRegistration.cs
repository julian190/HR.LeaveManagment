using HR.LeaveManagement.Identity.Models;
using HR.LeaveManagment.Application.Contracts.Identity;
using HR.LeaveManagment.Application.Models.Identity;
using HR.LeaveManagment.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HR.LeaveManagement.Identity
{
    public static class IdentityServicesRegistration
    {
        public static IServiceCollection ConfigurationIdentityServices (this IServiceCollection services , IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.AddDbContext<LeaveManagmentIdentityDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("LeaveManagmentConnectionString"),
                b => b.MigrationsAssembly(typeof(LeaveManagmentIdentityDbContext).Assembly.FullName)));
            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<LeaveManagmentIdentityDbContext>().AddDefaultTokenProviders();
            services.AddTransient<IAuthService, AuthService>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                };
            });
            return services;
        }
    }
}
