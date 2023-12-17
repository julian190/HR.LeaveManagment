using HR.LeaveManagment.Application.Contracts.Infrastructure;
using HR.LeaveManagment.Application.Models;
using HR.LeaveManagment.Infrastrucure.SendEmail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagment.Infrastrucure
{
    public static class InfrastrucureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastrucureServices(this IServiceCollection services ,IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, HR.LeaveManagment.Infrastrucure.SendEmail.SendEmail>();
            return services;
        }
    }
}
