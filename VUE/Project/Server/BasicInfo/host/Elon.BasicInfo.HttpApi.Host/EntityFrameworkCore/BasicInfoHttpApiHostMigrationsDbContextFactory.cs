using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Elon.BasicInfo.EntityFrameworkCore;

public class BasicInfoHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<BasicInfoHttpApiHostMigrationsDbContext>
{
    public BasicInfoHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<BasicInfoHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("BasicInfo"));

        return new BasicInfoHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
