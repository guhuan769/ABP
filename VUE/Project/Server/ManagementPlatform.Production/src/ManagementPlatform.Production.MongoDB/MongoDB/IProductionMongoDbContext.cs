using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace ManagementPlatform.Production.MongoDB;

[ConnectionStringName(ProductionDbProperties.ConnectionStringName)]
public interface IProductionMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
