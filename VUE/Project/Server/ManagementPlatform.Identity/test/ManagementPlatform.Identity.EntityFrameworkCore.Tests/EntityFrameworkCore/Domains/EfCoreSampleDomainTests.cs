using ManagementPlatform.Identity.Samples;
using Xunit;

namespace ManagementPlatform.Identity.EntityFrameworkCore.Domains;

[Collection(IdentityTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<IdentityEntityFrameworkCoreTestModule>
{

}
