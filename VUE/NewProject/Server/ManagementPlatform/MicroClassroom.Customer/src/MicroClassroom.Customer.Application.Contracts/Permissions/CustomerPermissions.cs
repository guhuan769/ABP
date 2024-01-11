using Volo.Abp.Reflection;

namespace MicroClassroom.Customer.Permissions;

public class CustomerPermissions
{
    public const string GroupName = "Customer";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(CustomerPermissions));
    }
}
