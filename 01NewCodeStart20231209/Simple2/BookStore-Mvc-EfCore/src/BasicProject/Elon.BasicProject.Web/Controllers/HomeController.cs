using Elon.BasicProject.Application.Contracts.Users;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Elon.BasicProject.Web.Controllers
{
    public class HomeController :Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRoleAppService _iUserAppService;
        public HomeController(ILogger<HomeController> logger,IRoleAppService userAppService)
        {
            _logger = logger;
            _iUserAppService = userAppService;
        }

        public IActionResult Index()
        {
            
            Console.WriteLine("****************************");
            Console.WriteLine("****************************");
            this._iUserAppService.GetUserAsync(123);
            Console.WriteLine($"This is {this.GetType().Name} {MethodInfo.GetCurrentMethod().Name} Invoke noticeId={1} ");
            Console.WriteLine("****************************");
            Console.WriteLine("****************************");
            return View();
        }
    }
}
