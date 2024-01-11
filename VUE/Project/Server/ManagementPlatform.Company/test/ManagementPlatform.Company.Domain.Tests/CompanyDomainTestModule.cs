using Volo.Abp.Modularity;

namespace ManagementPlatform.Company;

[DependsOn(
    typeof(CompanyDomainModule),
    typeof(CompanyTestBaseModule)
)]
public class CompanyDomainTestModule : AbpModule
{

}
