using Volo.Abp.Modularity;

namespace ManagementPlatform.Production;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class ProductionApplicationTestBase<TStartupModule> : ProductionTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
