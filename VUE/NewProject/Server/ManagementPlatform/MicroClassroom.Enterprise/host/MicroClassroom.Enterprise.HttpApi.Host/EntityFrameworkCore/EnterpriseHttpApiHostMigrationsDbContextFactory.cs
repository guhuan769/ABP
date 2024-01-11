using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MicroClassroom.Enterprise.EntityFrameworkCore;

public class EnterpriseHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<EnterpriseHttpApiHostMigrationsDbContext>
{
    public EnterpriseHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<EnterpriseHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Enterprise"));

        return new EnterpriseHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
