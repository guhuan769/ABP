using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace Elon.BasicInfo.MongoDB;

[DependsOn(
    typeof(BasicInfoApplicationTestModule),
    typeof(BasicInfoMongoDbModule)
)]
public class BasicInfoMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });
    }
}
