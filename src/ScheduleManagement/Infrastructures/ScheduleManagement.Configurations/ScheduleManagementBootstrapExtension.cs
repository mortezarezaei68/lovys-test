using Framework.Domain.UnitOfWork;
using Framework.Query;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ScheduleManagement.Command.Handlers;
using ScheduleManagement.Handlers;
using ScheduleManagement.Persistence.EF.Repositories;

namespace ScheduleManagement.Configurations
{
    public static class ScheduleManagementBootstrapExtension
    {
        public static IServiceCollection ScheduleManagementServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>),
                typeof(ScheduleManagementCommandHandlerMediatR<,>));
            services.Scan(scan => scan
                .FromAssemblyOf<BookingDateRepository>()
                .AddClasses(classes => classes.AssignableTo<IRepository>())
                .AsMatchingInterface()
                .WithScopedLifetime());
            services.Scan(scan => scan
                .FromAssemblyOf<AddBookingDateTimeCommandHandler>()
                .AddClasses(classes => classes.AssignableTo(typeof(IScheduleManagementCommandHandlerMediatR<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            // services.Scan(scan => scan
            //     .FromAssemblyOf<GetOrderByIdQueryHandler>()
            //     .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandlerMediatR<,>)))
            //     .AsImplementedInterfaces()
            //     .WithScopedLifetime());

            return services;
        }
    }
}