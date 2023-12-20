using Elon.ForumABPExample.Samples;
using Xunit;

namespace Elon.ForumABPExample.EntityFrameworkCore.Applications;

[Collection(ForumABPExampleTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<ForumABPExampleEntityFrameworkCoreTestModule>
{

}
