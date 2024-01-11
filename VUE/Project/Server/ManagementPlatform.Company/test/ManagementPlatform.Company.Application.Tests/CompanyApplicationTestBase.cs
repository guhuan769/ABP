using Volo.Abp.Modularity;

namespace ManagementPlatform.Company;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class CompanyApplicationTestBase<TStartupModule> : CompanyTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
