using sample2.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace sample2.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(sample2EntityFrameworkCoreModule),
    typeof(sample2ApplicationContractsModule)
    )]
public class sample2DbMigratorModule : AbpModule
{
}
