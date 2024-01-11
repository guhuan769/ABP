using Volo.Abp.Modularity;

namespace ManagementPlatform.Production;

[DependsOn(
    typeof(ProductionDomainModule),
    typeof(ProductionTestBaseModule)
)]
public class ProductionDomainTestModule : AbpModule
{

}
