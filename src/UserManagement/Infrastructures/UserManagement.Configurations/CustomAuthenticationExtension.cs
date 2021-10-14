using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Framework.EventBus;
using Framework.Exception.Exceptions;
using Framework.Exception.Exceptions.Enum;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using UserManagement.Commands.UserTokenCommands;

namespace UserManagement.Configurations
{
     public static class CustomAuthenticationExtension
    {
        public static void AddCustomAuthenticationAuthorization(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer("Bearer", options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = configuration["JwtToken:Issuer"],
                        ValidateIssuer = true,
                        ValidAudience = configuration["JwtToken:Audience"],
                        LifetimeValidator = (notBefore, expires, securityToken, validationParameter) =>
                            expires >= DateTime.UtcNow,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("x8{'5,C4ram)5zLq")),
                        // TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("x8{'5,C4ram)5zLq"))
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = async context =>
                        {
                            if (context.Request.Cookies.ContainsKey("X-Access-Token"))
                            {
                                context.Token =context.Request.Cookies["X-Access-Token"];
                            }
                            else if (context.Request.Cookies.ContainsKey("X-Refresh-Token"))
                            {
                                await RefreshToken(context, configuration);
                            }
                        }
                    };
                });
            services.ConfigureApplicationCookie(options => {
                options.Cookie.Domain = configuration["JwtToken:DomainUrl"];
                options.Cookie.Path = "/";
            });
            // services.AddAuthorization(options =>
            // {
            //     options.AddPolicy("SuperClient", builder =>
            //     {
            //         builder.RequireClaim("scope","api1");
            //     });
            //     // options.AddPolicy("superuser", builder =>
            //     // {
            //     //     builder.RequireRole("superuser");
            //     // });
            // });
            services.AddAuthorization();
        }

        private static async Task RefreshToken(MessageReceivedContext context, IConfiguration configuration)
        {
            var eventBus = context.HttpContext.RequestServices.GetRequiredService<IEventBus>();
            var command = new ExtendAccessTokenCommandRequest
            {
                RefreshToken = context.Request.Cookies["X-Refresh-Token"]
            };
            var data = await eventBus.Issue(command);
            if (!data.IsSuccess)
            {
                throw new AppException(ResultCode.BadRequest, data.Message,
                    HttpStatusCode.BadRequest);
            }

            context.Token = data.Data.AccessToken;
            var httpAccessor = context.HttpContext.RequestServices.GetRequiredService<IHttpContextAccessor>();
            httpAccessor.HttpContext?.Response.Cookies.Append("X-Refresh-Token", data.Data.RefreshToken,
                new CookieOptions
                {
                    Domain = configuration["JwtToken:DomainUrl"], HttpOnly = true,
                    SameSite = SameSiteMode.Lax,
                    Expires = DateTimeOffset.Now.AddDays(int.Parse(configuration["JwtToken:ExpirationDays"])),
                    Secure = false
                });
            httpAccessor.HttpContext?.Response.Cookies.Append("X-Access-Token", data.Data.AccessToken,
                new CookieOptions
                {
                    Domain = configuration["JwtToken:DomainUrl"], HttpOnly = true,
                    SameSite = SameSiteMode.Lax,
                    Expires =
                        DateTimeOffset.Now.AddSeconds(int.Parse(configuration["JwtToken:AccessTokenExpiredTime"])),
                    Secure = false
                });
        }
    }
}