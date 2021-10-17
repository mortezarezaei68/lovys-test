using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Command.Handlers.CandidateCommandHandlers;

namespace Framework.Controller.Extensions
{
    public static class ControllerExtension
    {
        public static void AddCustomController(this IServiceCollection services)
        {
            services
                .AddControllers(options => options.Filters.Add(typeof(ValidateModelStateAttribute)))
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<AddCustomerCommandRequestValidator>();

                })
                .AddJsonOptions(options => { options.JsonSerializerOptions.IgnoreNullValues = true; })
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
        }
    }
}