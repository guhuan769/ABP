using ManagementPlatform.Company.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace ManagementPlatform.Company.Permissions;

public class CompanyPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(CompanyPermissions.GroupName, L("Permission:Company"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CompanyResource>(name);
    }
}
