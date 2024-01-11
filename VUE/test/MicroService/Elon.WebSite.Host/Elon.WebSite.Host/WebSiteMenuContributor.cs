using Volo.Abp.UI.Navigation;

namespace Elon.WebSite.Host;
public class WebSiteMenuContributor : IMenuContributor
{
    public Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name != StandardMenus.Main)
        {
            return Task.CompletedTask;
        }

        //TODO: Localize menu items
        context.Menu.AddItem(new ApplicationMenuItem("App.Home", "Home", "/"));
        context.Menu.AddItem(new ApplicationMenuItem("App.Products", "Privacy", "/Privacy"));

        return Task.CompletedTask;
    }
}
