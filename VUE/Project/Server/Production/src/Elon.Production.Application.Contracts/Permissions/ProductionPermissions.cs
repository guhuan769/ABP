using Volo.Abp.Reflection;

namespace Elon.Production.Permissions;

public class ProductionPermissions
{
    public const string GroupName = "Production";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(ProductionPermissions));
    }
}
