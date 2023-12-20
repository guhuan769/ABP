using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Elon.ConfiguratioinCenter.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class ConfiguratioinCenterDbContextFactory : IDesignTimeDbContextFactory<ConfiguratioinCenterDbContext>
{
    public ConfiguratioinCenterDbContext CreateDbContext(string[] args)
    {
        ConfiguratioinCenterEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<ConfiguratioinCenterDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new ConfiguratioinCenterDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Elon.ConfiguratioinCenter.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
