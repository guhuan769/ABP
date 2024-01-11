using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace ManagementPlatform.Production.EntityFrameworkCore;

public class ProductionHttpApiHostMigrationsDbContext : AbpDbContext<ProductionHttpApiHostMigrationsDbContext>
{
    public ProductionHttpApiHostMigrationsDbContext(DbContextOptions<ProductionHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureProduction();
    }
}
