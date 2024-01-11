using Volo.Abp.Modularity;

namespace ManagementPlatform.Production;

[DependsOn(
    typeof(ProductionApplicationModule),
    typeof(ProductionDomainTestModule)
    )]
public class ProductionApplicationTestModule : AbpModule
{

}
