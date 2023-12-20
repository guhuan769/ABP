using Volo.Abp.Modularity;

namespace Elon.ForumABPExample;

public abstract class ForumABPExampleApplicationTestBase<TStartupModule> : ForumABPExampleTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
