using System;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using UserManagement.Domain;
using UserManagement.Persistance;
using UserManagement.Persistance.Context;
using UserManagement.Validations;

namespace UserManagement.Configurations
{
    public static class IdentityDbExtension
    {
        public static IServiceCollection IdentityDbInjections(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<UserManagementDbContext>(b =>
                b.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    mySqlOptions =>
                    {
                        mySqlOptions.CommandTimeout(120);
                    }));
            
            services.AddIdentity<User,Role>(options =>
                {
                    options.SignIn.RequireConfirmedEmail = true;
                    // options.Password.RequiredLength = 0;
                    // options.Password.RequireLowercase = false;
                    // options.Password.RequireUppercase = false;
                    // options.Password.RequireNonAlphanumeric = false;
                    // options.Password.RequireDigit = false;
                    // options.Password.RequiredUniqueChars = 0;
                } )
                // .AddPasswordValidator<CustomPasswordValidator>()
                .AddEntityFrameworkStores<UserManagementDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(2));
            services.AddTransient<IPasswordHasher<User>, CustomPasswordHasher>();
            return services;
        }
    }
}