using Volo.Abp.Settings;

namespace Elon.ForumABPExample.Settings;

public class ForumABPExampleSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(ForumABPExampleSettings.MySetting1));
    }
}
