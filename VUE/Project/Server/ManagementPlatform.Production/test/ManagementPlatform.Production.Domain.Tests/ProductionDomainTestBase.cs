using Volo.Abp.Modularity;

namespace ManagementPlatform.Production;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class ProductionDomainTestBase<TStartupModule> : ProductionTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
