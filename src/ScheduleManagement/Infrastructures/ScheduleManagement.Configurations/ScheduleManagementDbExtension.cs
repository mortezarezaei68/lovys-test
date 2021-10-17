using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScheduleManagement.Persistence.EF.Context;

namespace ScheduleManagement.Configurations
{
    public static class ScheduleManagementDbExtension
    {
        public static IServiceCollection ScheduleManagementDbInjections(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<ScheduleManagementDbContext>(b =>
                b.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    mySqlOptions =>
                    {
                        mySqlOptions.CommandTimeout(120);
                    }));
            return services;
        }
    }
}