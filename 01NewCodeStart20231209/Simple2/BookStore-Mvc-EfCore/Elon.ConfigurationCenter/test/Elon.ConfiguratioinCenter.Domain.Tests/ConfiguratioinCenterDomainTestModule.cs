using Elon.ConfiguratioinCenter.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Elon.ConfiguratioinCenter;

[DependsOn(
    typeof(ConfiguratioinCenterEntityFrameworkCoreTestModule)
    )]
public class ConfiguratioinCenterDomainTestModule : AbpModule
{

}
