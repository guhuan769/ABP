using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Elon.Production;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(ProductionDomainSharedModule)
)]
public class ProductionDomainModule : AbpModule
{

}
