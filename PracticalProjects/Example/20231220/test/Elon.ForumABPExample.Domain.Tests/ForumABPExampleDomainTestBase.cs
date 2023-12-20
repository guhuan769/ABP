using Volo.Abp.Modularity;

namespace Elon.ForumABPExample;

/* Inherit from this class for your domain layer tests. */
public abstract class ForumABPExampleDomainTestBase<TStartupModule> : ForumABPExampleTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
