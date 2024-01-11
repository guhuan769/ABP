using Volo.Abp.Reflection;

namespace ManagementPlatform.Production.Permissions;

public class ProductionPermissions
{
    public const string GroupName = "Production";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(ProductionPermissions));
    }
}
