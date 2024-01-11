using Volo.Abp.Modularity;

namespace ManagementPlatform.Company;

[DependsOn(
    typeof(CompanyApplicationModule),
    typeof(CompanyDomainTestModule)
    )]
public class CompanyApplicationTestModule : AbpModule
{

}
