using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace UserManagement.Persistence.EF.Context
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<UserManagementDbContext>
    {
        public UserManagementDbContext CreateDbContext(string[] args)
        {
            var config = GetAppSetting();
            var optionsBuilder = new DbContextOptionsBuilder<UserManagementDbContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            return new UserManagementDbContext(optionsBuilder.Options);
        }


        private IConfigurationRoot GetAppSetting()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var isDevelopment = environment == Environments.Development;
            var isStaging = environment == Environments.Staging;

            if (isDevelopment)
                return new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: false)
                    .Build();
            if (isStaging)
                return new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true)
                    .Build();

            return new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }
    }
}