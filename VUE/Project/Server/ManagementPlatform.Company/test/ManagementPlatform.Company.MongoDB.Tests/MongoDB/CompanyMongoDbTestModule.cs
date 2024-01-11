using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace ManagementPlatform.Company.MongoDB;

[DependsOn(
    typeof(CompanyApplicationTestModule),
    typeof(CompanyMongoDbModule)
)]
public class CompanyMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });
    }
}
