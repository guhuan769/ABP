using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Elon.BasicInfo;

[DependsOn(
    typeof(BasicInfoDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class BasicInfoApplicationContractsModule : AbpModule
{

}
