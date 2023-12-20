using Volo.Abp.Modularity;

namespace Elon.ForumABPExample;

[DependsOn(
    typeof(ForumABPExampleApplicationModule),
    typeof(ForumABPExampleDomainTestModule)
)]
public class ForumABPExampleApplicationTestModule : AbpModule
{

}
