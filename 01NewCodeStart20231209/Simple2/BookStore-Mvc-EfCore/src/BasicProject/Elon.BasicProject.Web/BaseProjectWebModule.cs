using Elon.BasicProject.Application;
using Elon.BasicProject.Application.Contracts;
using Elon.BasicProject.Application.Contracts.Users;
using Elon.BasicProject.Application.Users;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace Elon.BasicProject.Web
{
    /// <summary>
    ///  应用名称 -- 项目名
    /// </summary>
    [DependsOn(typeof(AbpAspNetCoreMvcModule))] // 通过特性声明依赖，会在程序启动的过程中，去加载先加载这个类库 (哭) 执行其初始化方法
    [DependsOn(typeof(BasicProjectApplicationContractsModule)
        , typeof(BasicProjectApplicationModule))] // Application  + Contracts
    public class BaseProjectWebModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //context.Services.AddSingleton<IUserAppService,UserAppService>();//常规理解是这样做的

            context.Services.AddSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "BasicProject API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);

                //// xml文档绝对路径 
                //var file = Path.Combine($"{AppContext.BaseDirectory}", "ABasicProject.Web.xml");
                //// true : 显示控制器层注释
                //options.IncludeXmlComments(file, true);


                //options.IncludeXmlComments(typeof(Program).Assembly.FullName);
                //添加xml
                //Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml").ToList().ForEach(file =>
                //{
                //    options.IncludeXmlComments(file, true);
                //});
            });

            //常规情况下，开发需要添加控制器---Action，但是也不干啥，然后企业去调用服务
            //有了 ABP的Auto API，直接自动编程了WEBAPI 省事
            //配置option--初始化配置  auto api
            base.Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(BasicProjectApplicationModule).Assembly);
            });
        }

        /// <summary>
        /// 代替 Configure --- 完成初始化
        /// </summary>
        /// <param name="context"></param>
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
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "BasicProject.Web");
            });
            #endregion
            app.UseAuthorization();
            app.UseConfiguredEndpoints();
        }
    }
}
