using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace ManagementPlatform.Company;

[DependsOn(
    typeof(CompanyDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class CompanyApplicationContractsModule : AbpModule
{

}
