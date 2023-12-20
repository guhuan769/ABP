using Volo.Abp.Modularity;

namespace Elon.ForumABPExample;

[DependsOn(
    typeof(ForumABPExampleDomainModule),
    typeof(ForumABPExampleTestBaseModule)
)]
public class ForumABPExampleDomainTestModule : AbpModule
{

}
