using Elon.Identity.Samples;
using Xunit;

namespace Elon.Identity.EntityFrameworkCore.Applications;

[Collection(IdentityTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<IdentityEntityFrameworkCoreTestModule>
{

}
