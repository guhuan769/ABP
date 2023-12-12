using Elon.BasicProject.Application.Contracts.Users;
using Elon.BasicProject.Application.Users;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Elon.BasicProject.Web.Controllers
{
    /// <summary>
    /// 实例化 HomeController, 依赖3个参数
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRoleAppService _iUserAppService;
        private readonly IServiceProvider _iServiceProvider;
        private readonly IRoleAppService _RoleserviceProvider;
        public HomeController(ILogger<HomeController> logger
            , IRoleAppService userAppService,
            IServiceProvider iServiceProvider,
            IRoleAppService RoleserviceProvider
            )
        {
            _logger = logger;
            _iUserAppService = userAppService;
            _iServiceProvider = iServiceProvider;
            _RoleserviceProvider = RoleserviceProvider;
        }

        public IActionResult Index()
        {
            Console.WriteLine("****************************");
            Console.WriteLine("****************************");
            var user = this._iUserAppService.GetUserAsync(123);
            Console.WriteLine($"{user.Result.UserName}");

            var service = this._iServiceProvider.GetService<UserAppService>();
            var user1 = service.GetUserAsync(123);

            Console.WriteLine($"This is {this.GetType().Name} {MethodInfo.GetCurrentMethod().Name} Invoke noticeId={1} ");
            Console.WriteLine("****************************");
            Console.WriteLine("****************************");
           
            return View();
        }
    }
}
