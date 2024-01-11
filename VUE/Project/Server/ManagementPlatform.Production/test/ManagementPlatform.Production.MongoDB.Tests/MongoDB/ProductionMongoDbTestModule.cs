using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace ManagementPlatform.Production.MongoDB;

[DependsOn(
    typeof(ProductionApplicationTestModule),
    typeof(ProductionMongoDbModule)
)]
public class ProductionMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });
    }
}
