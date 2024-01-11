using Volo.Abp.Modularity;

namespace Elon.Identity;

[DependsOn(
    typeof(IdentityDomainModule),
    typeof(IdentityTestBaseModule)
)]
public class IdentityDomainTestModule : AbpModule
{

}
