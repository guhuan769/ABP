using Volo.Abp.Reflection;

namespace ManagementPlatform.Company.Permissions;

public class CompanyPermissions
{
    public const string GroupName = "Company";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(CompanyPermissions));
    }
}
