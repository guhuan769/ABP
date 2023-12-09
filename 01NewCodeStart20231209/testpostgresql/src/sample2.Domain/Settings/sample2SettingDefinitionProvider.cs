using Volo.Abp.Settings;

namespace sample2.Settings;

public class sample2SettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(sample2Settings.MySetting1));
    }
}
