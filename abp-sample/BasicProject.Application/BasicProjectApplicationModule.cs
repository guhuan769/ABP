using BasicProject.Application.Contracts;
using BasicProject.Application.Contracts.Users;
using BasicProject.Application.Users;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Modularity;

namespace BasicProject.Application
{
    [DependsOn(typeof(BasicProjectApplicationContractsModule))]
    public class BasicProjectApplicationModule:AbpModule,IRemoteService//, IApplicationService
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //这才是模块化的做法
            context.Services.AddSingleton<IUserAppService, UserAppService>();
        }
    }
}