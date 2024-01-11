using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace MicroClassroom.Enterprise.MongoDB;

[ConnectionStringName(EnterpriseDbProperties.ConnectionStringName)]
public class EnterpriseMongoDbContext : AbpMongoDbContext, IEnterpriseMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureEnterprise();
    }
}
