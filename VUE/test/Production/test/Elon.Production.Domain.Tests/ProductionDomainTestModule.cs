using Volo.Abp.Modularity;

namespace Elon.Production;

[DependsOn(
    typeof(ProductionDomainModule),
    typeof(ProductionTestBaseModule)
)]
public class ProductionDomainTestModule : AbpModule
{

}
