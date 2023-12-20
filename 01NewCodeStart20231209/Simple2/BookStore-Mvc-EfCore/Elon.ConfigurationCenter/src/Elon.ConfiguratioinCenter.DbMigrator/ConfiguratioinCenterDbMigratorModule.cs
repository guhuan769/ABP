using Elon.ConfiguratioinCenter.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Elon.ConfiguratioinCenter.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ConfiguratioinCenterEntityFrameworkCoreModule),
    typeof(ConfiguratioinCenterApplicationContractsModule)
    )]
public class ConfiguratioinCenterDbMigratorModule : AbpModule
{
}
