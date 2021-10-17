using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ScheduleManagement.Configurations
{
    public static class ScheduleServicesManagement
    {
        public static void ScheduleServicesManagementExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.ScheduleManagementDbInjections(configuration);
            services.BootstrapEventScheduleManagement(configuration);
            services.ScheduleManagementServices();
        }
    }
}