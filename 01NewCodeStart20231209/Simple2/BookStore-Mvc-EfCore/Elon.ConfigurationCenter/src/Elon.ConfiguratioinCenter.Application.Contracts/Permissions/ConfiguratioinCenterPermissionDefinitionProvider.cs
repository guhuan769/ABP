using Elon.ConfiguratioinCenter.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Elon.ConfiguratioinCenter.Permissions;

public class ConfiguratioinCenterPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ConfiguratioinCenterPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(ConfiguratioinCenterPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ConfiguratioinCenterResource>(name);
    }
}
