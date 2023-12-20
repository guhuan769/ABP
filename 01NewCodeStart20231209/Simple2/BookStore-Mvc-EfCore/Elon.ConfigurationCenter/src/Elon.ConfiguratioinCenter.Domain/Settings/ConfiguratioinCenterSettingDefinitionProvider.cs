using Volo.Abp.Settings;

namespace Elon.ConfiguratioinCenter.Settings;

public class ConfiguratioinCenterSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(ConfiguratioinCenterSettings.MySetting1));
    }
}
