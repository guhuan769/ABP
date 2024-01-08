using Volo.Abp.Modularity;

namespace Elon.Identity;

public abstract class IdentityApplicationTestBase<TStartupModule> : IdentityTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
