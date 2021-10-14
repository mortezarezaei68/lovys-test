using System;
using Framework.EventBus;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Persistence.EF.UnitOfWork;

namespace UserManagement.Configurations
{
    public static class EventExtension
    {
        public static IServiceCollection BootstrapEventBusServices(this IServiceCollection services,IConfiguration configuration)
        {
            // services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            // services.AddTransient<ISendingEmail, SendingEmail>();
            services.AddTransient(typeof(IdentityUnitOfWork));
            services.AddScoped<IIdentityUnitOfWork,IdentityUnitOfWork>();
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IEventBus, EventBus>();
            return services;
        }
    }
}