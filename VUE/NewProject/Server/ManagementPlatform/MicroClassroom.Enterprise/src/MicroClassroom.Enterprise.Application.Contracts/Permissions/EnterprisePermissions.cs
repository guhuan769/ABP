using Volo.Abp.Reflection;

namespace MicroClassroom.Enterprise.Permissions;

public class EnterprisePermissions
{
    public const string GroupName = "Enterprise";

    public static class Mechanism
    {
        public const string Default = GroupName + ".MechanismManagement";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static class Courses
    {
        public const string Default = GroupName + ".CourseManagement";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static class Teachers
    {
        public const string Default = GroupName + ".TeacherManagement";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static class Banners
    {
        public const string Default = GroupName + ".BannerManagement";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(EnterprisePermissions));
    }
}
