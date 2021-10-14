using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Domain;
using UserManagement.Persistance.Context;

namespace UserManagement.Configurations
{
    public static class SeedIdentityData
    {
        public static async Task InitializeUserDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var identityServerDbContext = serviceScope.ServiceProvider.GetRequiredService<UserManagementDbContext>();
            identityServerDbContext.Database.Migrate();
            var roleManager = (RoleManager<Role>)serviceScope.ServiceProvider.GetService(typeof(RoleManager<Role>));
            var userManager = (UserManager<User>)serviceScope.ServiceProvider.GetService(typeof(UserManager<User>));

            var superuserRoleIsExist = await roleManager.FindByNameAsync("superuser");
            if (superuserRoleIsExist is null)
            {
                await roleManager.CreateAsync(new Role
                {
                    Name = "superuser"
                });
            }
            
            
            
            var superUserIfExist = await userManager.FindByNameAsync("superuser");
            if (superUserIfExist is null)
            {
                var user = new User()
                {
                    FirstName = "superuser",
                    LastName = "superuser",
                    Email = "superuser@test.com",
                    UserName = "superuser",
                    PhoneNumber = "+111111111",
                    SubjectId = Guid.NewGuid(),
                    UserType = UserType.Interviewer
                    
                };
                
                var userIsRegister = await userManager.CreateAsync(user, "_AcRcSwXRTUaRhHe7r?z");
                if (userIsRegister.Succeeded)
                {
                    var superUser = await userManager.FindByNameAsync("superuser");
                    await userManager.AddToRoleAsync(superUser, "superuser");
                }
                    
            }


        }
    }
}