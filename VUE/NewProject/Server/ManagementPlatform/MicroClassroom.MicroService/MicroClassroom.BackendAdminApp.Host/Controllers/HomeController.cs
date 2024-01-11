using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace MicroClassroom.BackendAdminApp.Host.Controllers;

public class HomeController : AbpController
{
    public ActionResult Index()
    {
        return Redirect("/Identity/Users");
    }
}
