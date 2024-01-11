using MicroClassroom.Enterprise.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace MicroClassroom.BackendAdminApp.Host;

public abstract class BackendAdminAppPageModel : AbpPageModel
{
    protected BackendAdminAppPageModel()
    {
        LocalizationResourceType = typeof(EnterpriseResource);
    }
}
