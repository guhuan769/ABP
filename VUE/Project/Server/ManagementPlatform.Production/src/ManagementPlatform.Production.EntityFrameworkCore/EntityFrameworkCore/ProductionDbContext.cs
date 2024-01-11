using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace ManagementPlatform.Production.EntityFrameworkCore;

[ConnectionStringName(ProductionDbProperties.ConnectionStringName)]
public class ProductionDbContext : AbpDbContext<ProductionDbContext>, IProductionDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public ProductionDbContext(DbContextOptions<ProductionDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureProduction();
    }
}
