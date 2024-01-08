using Elon.Identity.Samples;
using Xunit;

namespace Elon.Identity.EntityFrameworkCore.Domains;

[Collection(IdentityTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<IdentityEntityFrameworkCoreTestModule>
{

}
