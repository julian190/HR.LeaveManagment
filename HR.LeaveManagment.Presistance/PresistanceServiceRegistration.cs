using HR.LeaveManagment.Application.Contracts.Presistance;
using HR.LeaveManagment.Presistance.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HR.LeaveManagment.Presistance
{
    public static class PresistanceServiceRegistration
    {
        public static IServiceCollection ConfigurePresistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LeaveManagmentDbContext>(optionsAction =>
            optionsAction.UseSqlServer(configuration.GetConnectionString("LeaveManagmentConnectionString"))
            );
            services.AddScoped (typeof(IGenericRepository<>),typeof(GenericRepository<>) );
            services.AddScoped <ILeaveTypeRepository,LeaveTypeRepository>();
            services.AddScoped<ILeaveRequestRepository, LeaveRequesRepository>();
            services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();

            return services;
             
        }
    }
}
