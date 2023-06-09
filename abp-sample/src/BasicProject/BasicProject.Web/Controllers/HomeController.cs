using BasicProject.Application.Contracts.Users;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace BasicProject.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserAppService _iUserAppService;
        public HomeController(ILogger<HomeController> logger, IUserAppService iUserAppService)
        {
            _logger = logger;
            _iUserAppService = iUserAppService;
        }

        [HttpPost]
        public Task<bool> Index()
        {
            Console.WriteLine("****************************");
            Console.WriteLine("****************************");
            Console.WriteLine($"This is {this.GetType().Name} {MethodBase.GetCurrentMethod().Name}" +
                $"");
            Console.WriteLine("****************************");
            Console.WriteLine("****************************");
            //var user = this._iUserAppService.GetUserAsync(123);
            Console.WriteLine();
            return Task.FromResult(true);
        }
    }
}
