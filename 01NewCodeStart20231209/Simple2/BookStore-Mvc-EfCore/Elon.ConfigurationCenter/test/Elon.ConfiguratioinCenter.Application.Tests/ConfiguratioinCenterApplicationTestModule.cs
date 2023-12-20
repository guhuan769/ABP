using Volo.Abp.Modularity;

namespace Elon.ConfiguratioinCenter;

[DependsOn(
    typeof(ConfiguratioinCenterApplicationModule),
    typeof(ConfiguratioinCenterDomainTestModule)
    )]
public class ConfiguratioinCenterApplicationTestModule : AbpModule
{

}
