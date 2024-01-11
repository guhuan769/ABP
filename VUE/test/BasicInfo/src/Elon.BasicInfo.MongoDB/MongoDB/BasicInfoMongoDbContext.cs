using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Elon.BasicInfo.MongoDB;

[ConnectionStringName(BasicInfoDbProperties.ConnectionStringName)]
public class BasicInfoMongoDbContext : AbpMongoDbContext, IBasicInfoMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureBasicInfo();
    }
}
