using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Elon.ConfiguratioinCenter.Pages;

public class Index_Tests : ConfiguratioinCenterWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
