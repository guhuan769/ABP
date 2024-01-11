using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace ManagementPlatform.Production.EntityFrameworkCore;

[ConnectionStringName(ProductionDbProperties.ConnectionStringName)]
public interface IProductionDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
