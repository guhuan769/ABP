using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Elon.ForumABPExample.Pages;

public class Index_Tests : ForumABPExampleWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
