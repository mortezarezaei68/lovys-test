using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Common;
using Framework.Controller.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ScheduleManagement.Configurations;
using UserManagement.Configurations;

namespace WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.UserServicesManagementExtension(Configuration);
            services.ScheduleServicesManagementExtension(Configuration);
            services.AddCustomController();
            services.AddCustomSwagger();
            services.AddCors(option =>
            {
                option.AddPolicy("EnableCorsForHttpOnly", builder =>
                {
                    builder.WithOrigins(
                            "https://localhost:5001",
                            "http://localhost",
                            "http://localhost:8080")
                        .AllowCredentials()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCustomExceptionHandler();
            app.UseCustomSwagger();
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.InitializeUserDatabase().Wait();
            app.InitializeScheduleDatabase().Wait();
            app.UseRouting();
            app.UseCors("EnableCorsForHttpOnly");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}