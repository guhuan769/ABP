using Microsoft.AspNetCore.Mvc;

namespace MicroClassroom.BackendAdminApp.Host.Pages.BannerManagement;

public class IndexModel : BackendAdminAppPageModel
{
    public virtual Task<IActionResult> OnGetAsync()
    {
        return Task.FromResult<IActionResult>(Page());
    }

    public virtual Task<IActionResult> OnPostAsync()
    {
        return Task.FromResult<IActionResult>(Page());
    }
}
