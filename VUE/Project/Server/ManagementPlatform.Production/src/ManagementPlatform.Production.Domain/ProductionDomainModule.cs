using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace ManagementPlatform.Production;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(ProductionDomainSharedModule)
)]
public class ProductionDomainModule : AbpModule
{

}
