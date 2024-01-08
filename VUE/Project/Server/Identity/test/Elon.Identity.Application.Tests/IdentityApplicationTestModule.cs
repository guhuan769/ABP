using Volo.Abp.Modularity;

namespace Elon.Identity;

[DependsOn(
    typeof(IdentityApplicationModule),
    typeof(IdentityDomainTestModule)
)]
public class IdentityApplicationTestModule : AbpModule
{

}
