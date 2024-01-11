using Volo.Abp.Modularity;

namespace Elon.BasicInfo;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class BasicInfoDomainTestBase<TStartupModule> : BasicInfoTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
