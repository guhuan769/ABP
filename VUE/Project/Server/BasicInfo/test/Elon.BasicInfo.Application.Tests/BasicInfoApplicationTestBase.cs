using Volo.Abp.Modularity;

namespace Elon.BasicInfo;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class BasicInfoApplicationTestBase<TStartupModule> : BasicInfoTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
