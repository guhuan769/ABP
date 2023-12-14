using Elon.DashboardCenter.Application;
using Elon.DashboardCenter.Application.Contracts;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace Elon.DashboardCenter.Web
{
    [DependsOn(typeof(AbpAspNetCoreMvcModule))]
    [DependsOn(typeof(DashboardCenterApplicationContractsModule)
        , typeof(DashboardCenterApplicationModule))]
    public class DashboardCenterWebModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //context.Services.AddSingleton<IUserAppService,UserAppService>();//常规理解是这样做的

            context.Services.AddSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "DashboardCenter API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });

            //常规情况下，开发需要添加控制器---Action，但是也不干啥，然后企业去调用服务
            //有了 ABP的Auto API，直接自动编程了WEBAPI 省事
            //配置option--初始化配置  auto api
            base.Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(DashboardCenterApplicationModule).Assembly);
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSwagger();
            #region 配置 swagger

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "DashboardCenter.Web");
            });
            #endregion
            app.UseAuthorization();
            app.UseConfiguredEndpoints();
        }

    }
}
