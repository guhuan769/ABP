using Elon.Production.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Elon.Production.Permissions;

public class ProductionPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ProductionPermissions.GroupName, L("Permission:Production"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ProductionResource>(name);
    }
}
