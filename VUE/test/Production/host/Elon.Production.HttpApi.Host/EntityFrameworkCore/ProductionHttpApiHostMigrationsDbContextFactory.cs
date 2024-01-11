using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Elon.Production.EntityFrameworkCore;

public class ProductionHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<ProductionHttpApiHostMigrationsDbContext>
{
    public ProductionHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<ProductionHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Production"));

        return new ProductionHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
