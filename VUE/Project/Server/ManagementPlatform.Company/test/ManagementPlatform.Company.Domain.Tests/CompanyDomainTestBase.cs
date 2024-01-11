using Volo.Abp.Modularity;

namespace ManagementPlatform.Company;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class CompanyDomainTestBase<TStartupModule> : CompanyTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
