using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ScheduleManagement.Persistence.EF.Context;

namespace ScheduleManagement.Configurations
{
    public static class SeedScheduleData
    {
        public static async Task InitializeScheduleDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var serverDbContext = serviceScope.ServiceProvider.GetRequiredService<ScheduleManagementDbContext>();
            await serverDbContext.Database.MigrateAsync();
            

        }
    }
}