using MicroClassroom.Enterprise.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace MicroClassroom.Enterprise.Permissions;

public class EnterprisePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(EnterprisePermissions.GroupName, L("Permission:Enterprise"));

        var mechanismPermission = myGroup.AddPermission(EnterprisePermissions.Mechanism.Default, L("MechanismManagement"));
        mechanismPermission.AddChild(EnterprisePermissions.Mechanism.Create, L("Permission:Create"));
        mechanismPermission.AddChild(EnterprisePermissions.Mechanism.Update, L("Permission:Edit"));
        mechanismPermission.AddChild(EnterprisePermissions.Mechanism.Delete, L("Permission:Delete"));

        var coursePermission = myGroup.AddPermission(EnterprisePermissions.Courses.Default, L("CourseManagement"));
        coursePermission.AddChild(EnterprisePermissions.Courses.Create, L("Permission:Create"));
        coursePermission.AddChild(EnterprisePermissions.Courses.Update, L("Permission:Edit"));
        coursePermission.AddChild(EnterprisePermissions.Courses.Delete, L("Permission:Delete"));

        var teacherPermission = myGroup.AddPermission(EnterprisePermissions.Teachers.Default, L("TeacherManagement"));
        teacherPermission.AddChild(EnterprisePermissions.Teachers.Create, L("Permission:Create"));
        teacherPermission.AddChild(EnterprisePermissions.Teachers.Update, L("Permission:Edit"));
        teacherPermission.AddChild(EnterprisePermissions.Teachers.Delete, L("Permission:Delete"));

        var bannerPermission = myGroup.AddPermission(EnterprisePermissions.Banners.Default, L("BannerManagement"));
        bannerPermission.AddChild(EnterprisePermissions.Banners.Create, L("Permission:Create"));
        bannerPermission.AddChild(EnterprisePermissions.Banners.Update, L("Permission:Edit"));
        bannerPermission.AddChild(EnterprisePermissions.Banners.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<EnterpriseResource>(name);
    }
}
