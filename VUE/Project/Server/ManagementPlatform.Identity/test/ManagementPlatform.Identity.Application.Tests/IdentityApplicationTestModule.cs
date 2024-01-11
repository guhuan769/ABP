using Volo.Abp.Modularity;

namespace ManagementPlatform.Identity;

[DependsOn(
    typeof(IdentityApplicationModule),
    typeof(IdentityDomainTestModule)
)]
public class IdentityApplicationTestModule : AbpModule
{

}
