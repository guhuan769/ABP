using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ManagementPlatform.Company.EntityFrameworkCore;

public class CompanyHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<CompanyHttpApiHostMigrationsDbContext>
{
    public CompanyHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<CompanyHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Company"));

        return new CompanyHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
