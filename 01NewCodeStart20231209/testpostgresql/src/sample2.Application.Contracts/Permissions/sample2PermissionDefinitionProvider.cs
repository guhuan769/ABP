using sample2.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace sample2.Permissions;

public class sample2PermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(sample2Permissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(sample2Permissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<sample2Resource>(name);
    }
}
