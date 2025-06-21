using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AdvancedTodoApp.Persistence.Context;
using System.Runtime.InteropServices;

namespace AdvancedTodoApp.Persistence.Context
{
    /// <summary>
    /// This factory is only used by EF core CLI tools during design-time for migrations. It manually builds AppDbContext without needing to run full application.
    /// Note: EF CLI commands should always be run in the project where the target is located(Usually the API layer because it contains the startup and configurations like appsettings.json, DI setup, connection string etc)
    /// It will have access to this even if this is written in the persistence layer.
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args) {
            {
                // Get folder where command is run from (Should be API project)
                var basePath = Directory.GetCurrentDirectory();

                // Build the config reading appsettings.json 
                var config = new ConfigurationBuilder()
                        .SetBasePath(basePath)
                        .AddJsonFile("appsettings.json", optional: false)
                        .Build();

                // get conn string
                var connectionString = config.GetConnectionString("DefaultConnection");

                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");
                }

                // create DbContext 
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlServer(connectionString);

                return new AppDbContext(optionsBuilder.Options);
            }
        }
    }
}
