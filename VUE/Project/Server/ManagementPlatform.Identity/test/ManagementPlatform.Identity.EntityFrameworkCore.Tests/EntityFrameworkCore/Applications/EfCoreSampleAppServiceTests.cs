using ManagementPlatform.Identity.Samples;
using Xunit;

namespace ManagementPlatform.Identity.EntityFrameworkCore.Applications;

[Collection(IdentityTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<IdentityEntityFrameworkCoreTestModule>
{

}
