using Elon.BasicInfo.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Elon.BasicInfo.Permissions;

public class BasicInfoPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(BasicInfoPermissions.GroupName, L("Permission:BasicInfo"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BasicInfoResource>(name);
    }
}
