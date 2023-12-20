using Elon.ForumABPExample.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Elon.ForumABPExample.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ForumABPExampleEntityFrameworkCoreModule),
    typeof(ForumABPExampleApplicationContractsModule)
    )]
public class ForumABPExampleDbMigratorModule : AbpModule
{
}
