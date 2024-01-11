using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace ManagementPlatform.Production.MongoDB;

[ConnectionStringName(ProductionDbProperties.ConnectionStringName)]
public class ProductionMongoDbContext : AbpMongoDbContext, IProductionMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureProduction();
    }
}
