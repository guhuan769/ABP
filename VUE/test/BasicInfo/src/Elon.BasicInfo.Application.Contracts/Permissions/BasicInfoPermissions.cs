using Volo.Abp.Reflection;

namespace Elon.BasicInfo.Permissions;

public class BasicInfoPermissions
{
    public const string GroupName = "BasicInfo";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(BasicInfoPermissions));
    }
}
