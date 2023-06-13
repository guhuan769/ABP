using BasicProject.Application.Contracts.Users;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Volo.Abp.AspNetCore.Mvc;

namespace BasicProject.Web.Controllers
{
    /// <summary>
    /// 实例化HOmeController，依赖3个参数-4个---参数怎么来的？细思恐极
    /// 确实有这么个问题，IOC创建实例的时候会依赖其他对象，甚至多个，还有对象依赖对象
    /// 所以，得能这样:构造A时，看构造函数依赖B；先去构造B，发现B依赖C；再选构造C
    /// 构造C，拿去构造B，拿去构造A 出来了  
    /// </summary>
    //[ApiController]
    //[Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserAppService _iUserAppService;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="iUserAppService"></param>
        /// <param name="serviceProvider"></param>
        public HomeController(ILogger<HomeController> logger, IUserAppService iUserAppService, IServiceProvider serviceProvider = null)
        {
            _logger = logger;
            _iUserAppService = iUserAppService;
            _serviceProvider = serviceProvider;
        }

        //[HttpPost]
        public IActionResult TestIndex()
        {
            Console.WriteLine("****************************");
            Console.WriteLine("****************************");
            Console.WriteLine($"This is {this.GetType().Name} {MethodBase.GetCurrentMethod().Name}" +
                $"");
            Console.WriteLine("****************************");
            Console.WriteLine("****************************");
            var user = this._iUserAppService.GetUserAsync(123);
            Console.WriteLine();

            var service = this._serviceProvider.GetServices<IUserAppService>();
            var user1 = this._iUserAppService.GetUserAsync(123);
            Console.WriteLine($"{user1.Result.UserName}");

            return View();
        }
    }
}
