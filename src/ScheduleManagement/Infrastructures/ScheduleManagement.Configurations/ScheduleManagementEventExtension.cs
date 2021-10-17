using Framework.EventBus;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScheduleManagement.Handlers;
using ScheduleManagement.Persistence.EF.UnitOfWork;
using ScheduleManagement.Validations;

namespace ScheduleManagement.Configurations
{
    public static class ScheduleManagementEventExtension
    {
        public static IServiceCollection BootstrapEventScheduleManagement(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddTransient(typeof(ScheduleManagementUnitOfWork));
            services.AddScoped<IScheduleManagementUnitOfWork,ScheduleManagementUnitOfWork>();
            services.AddScoped<IBookingDateValidationService, BookingDateValidationService>();
            services.AddMediatR(typeof(IScheduleManagementCommandHandlerMediatR<,>).Assembly);
            services.AddScoped<IEventBus, EventBus>();
            return services;
        }
    }
}