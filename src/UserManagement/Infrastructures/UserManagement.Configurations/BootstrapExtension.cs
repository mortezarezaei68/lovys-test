
using Framework.Common;
using Framework.Domain.UnitOfWork;
using Framework.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Service.Query.AdminUserQuery;
using UserManagement.Command.Handlers.CandidateCommandHandlers;
using UserManagement.Command.Handlers.InterviewerUserCommandHandlers;
using UserManagement.Handlers;
using UserManagement.Persistence.EF.Repository;

namespace UserManagement.Configurations
{
    public static class BootstrapExtension
    {
        public static IServiceCollection BootstrapCustomizeServices(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICurrentUser, CurrentUser>();

            services.AddTransient(typeof(IPipelineBehavior<,>),
                typeof(UserManagementCommandHandlerMediatR<,>));
            
            services.Scan(scan => scan
                .FromAssemblyOf<PersistGrantsRepository>()
                .AddClasses(classes => classes.AssignableTo<IRepository>())
                .AsMatchingInterface()
                .WithScopedLifetime());
            services.Scan(scan => scan
                .FromAssemblyOf<AddCandidateCommandHandler>()
                .AddClasses(classes => classes.AssignableTo(typeof(IUserManagementCommandHandlerMediatR<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            // services.Scan(scan => scan
            //     .FromAssemblyOf<GetAllAdminUsersQueryHandler>()
            //     .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandlerMediatR<,>)))
            //     .AsImplementedInterfaces()
            //     .WithScopedLifetime());
            services.Scan(scan => scan
                .FromAssemblyOf<GetAllAdminUsersQueryHandler>()
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            return services;
        }
    }
}