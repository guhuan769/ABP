using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace MicroClassroom.Enterprise;

[DependsOn(
    typeof(EnterpriseDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class EnterpriseApplicationContractsModule : AbpModule
{

}
