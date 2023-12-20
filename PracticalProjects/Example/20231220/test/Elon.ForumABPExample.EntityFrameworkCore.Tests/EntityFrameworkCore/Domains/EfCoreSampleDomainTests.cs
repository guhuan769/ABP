using Elon.ForumABPExample.Samples;
using Xunit;

namespace Elon.ForumABPExample.EntityFrameworkCore.Domains;

[Collection(ForumABPExampleTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<ForumABPExampleEntityFrameworkCoreTestModule>
{

}
