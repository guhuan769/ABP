using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace ManagementPlatform.Company.MongoDB;

[ConnectionStringName(CompanyDbProperties.ConnectionStringName)]
public class CompanyMongoDbContext : AbpMongoDbContext, ICompanyMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {

        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureCompany();

    }
}
