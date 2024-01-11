using Volo.Abp.Modularity;

namespace ManagementPlatform.Identity;

[DependsOn(
    typeof(IdentityDomainModule),
    typeof(IdentityTestBaseModule)
)]
public class IdentityDomainTestModule : AbpModule
{

}
