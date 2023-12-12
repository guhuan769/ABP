using Elon.BasicProject.Application.Contracts;
using Elon.BasicProject.Application.Contracts.Users;
using Elon.BasicProject.Application.Users;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Elon.BasicProject.Application
{
    [DependsOn(typeof(BasicProjectApplicationContractsModule))]
    public class BasicProjectApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //context.Services.AddTransient<IUserAppService, UserAppService>();// 这才是模块化的做法


        }
    }
}
