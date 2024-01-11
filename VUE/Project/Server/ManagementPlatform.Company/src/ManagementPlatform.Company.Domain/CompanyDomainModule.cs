using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace ManagementPlatform.Company;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(CompanyDomainSharedModule)
)]
public class CompanyDomainModule : AbpModule
{

}
