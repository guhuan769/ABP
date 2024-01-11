using Volo.Abp.UI.Navigation;

namespace MicroClassroom.PublicApp.Host;

public class PublicAppMenuContributor : IMenuContributor
{
    public Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name != StandardMenus.Main)
        {
            return Task.CompletedTask;
        }

        //TODO: Localize menu items
        context.Menu.AddItem(new ApplicationMenuItem("App.Home", "Home", "/"));

        return Task.CompletedTask;
    }
}
