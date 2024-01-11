using Volo.Abp.Modularity;

namespace Elon.Production;

[DependsOn(
    typeof(ProductionApplicationModule),
    typeof(ProductionDomainTestModule)
    )]
public class ProductionApplicationTestModule : AbpModule
{

}
