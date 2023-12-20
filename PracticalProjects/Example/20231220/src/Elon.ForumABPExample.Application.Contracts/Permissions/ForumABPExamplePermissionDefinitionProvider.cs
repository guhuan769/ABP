using Elon.ForumABPExample.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Elon.ForumABPExample.Permissions;

public class ForumABPExamplePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ForumABPExamplePermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(ForumABPExamplePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ForumABPExampleResource>(name);
    }
}
