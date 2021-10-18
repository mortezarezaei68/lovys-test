using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UserManagement.Configurations
{
    public static class UserServicesManagement
    {
        public static void UserServicesManagementExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.IdentityDbInjections(configuration);
            services.BootstrapEventBusServices(configuration);
            services.BootstrapCustomizeServices();
            services.AddCustomAuthenticationAuthorization(configuration);
        }
    }
}