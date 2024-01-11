using MicroClassroom.Enterprise.Localization;
using MicroClassroom.Enterprise.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.MultiTenancy;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;

namespace MicroClassroom.BackendAdminApp.Host;

public class BackendAdminAppMenuContributor : IMenuContributor
{
    private readonly IConfiguration _configuration;

    public BackendAdminAppMenuContributor(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.User)
        {
            await ConfigureUserMenuAsync(context);
        }

        await ConfigureEnterpriseMenu(context);
    }

    private Task ConfigureEnterpriseMenu(MenuConfigurationContext context)
    {
        if (context.Menu.Name != StandardMenus.Main)
        {
            return Task.CompletedTask;
        }

        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<EnterpriseResource>();
        //var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();
        var currentTenant = context.ServiceProvider.GetRequiredService<ICurrentTenant>();
        if (currentTenant.IsAvailable)
        {
            var mechanismMenuItem = new ApplicationMenuItem(
                "MechanismInfo",
                l["MechanismInfo"],
                $"~/MechanismManagement/Info?id={currentTenant.Id}",
                icon: "fas fa-list-ul",
                order: 11
            );

            administration.AddItem(mechanismMenuItem).RequirePermissions(EnterprisePermissions.Mechanism.Update);
        }
        else 
        {
            var mechanismMenuItem = new ApplicationMenuItem(
                "MechanismManagement",
                l["MechanismManagement"],
                "~/MechanismManagement",
                icon: "fas fa-list-ul",
                order: 11
            );

            administration.AddItem(mechanismMenuItem).RequirePermissions(EnterprisePermissions.Mechanism.Default);
        }

        var courseMenuItem = new ApplicationMenuItem(
            "CourseManagement",
            l["CourseManagement"],
            "~/CourseManagement",
            icon: "fas fa-list-ul",
            order: 12
        );
        var teacherMenuItem = new ApplicationMenuItem(
            "TeacherManagement",
            l["TeacherManagement"],
            "~/TeacherManagement",
            icon: "fas fa-list-ul",
            order: 13
        );
        var bannerMenuItem = new ApplicationMenuItem(
            "BannerManagement",
            l["BannerManagement"],
            "~/BannerManagement",
            icon: "fas fa-list-ul",
            order: 14
        );


        administration.AddItem(courseMenuItem).RequirePermissions(EnterprisePermissions.Courses.Default);
        administration.AddItem(teacherMenuItem).RequirePermissions(EnterprisePermissions.Teachers.Default); ;
        administration.AddItem(bannerMenuItem).RequirePermissions(EnterprisePermissions.Banners.Default); ;

        return Task.CompletedTask;
    }

    private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
    {
        var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();

        var identityServerUrl = _configuration["AuthServer:Authority"] ?? "";

        if (currentUser.IsAuthenticated)
        {
            //TODO: Localize menu items
            context.Menu.AddItem(new ApplicationMenuItem("Account.Manage", "Manage Your Profile", $"{identityServerUrl.EnsureEndsWith('/')}Account/Manage", icon: "fa fa-cog", order: 1000, null, "_blank"));
            context.Menu.AddItem(new ApplicationMenuItem("Account.Logout", "Logout", url: "/Account/Logout", icon: "fa fa-power-off", order: int.MaxValue - 1000));
        }

        return Task.CompletedTask;
    }
}
