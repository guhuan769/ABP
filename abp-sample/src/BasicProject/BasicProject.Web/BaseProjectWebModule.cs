using Autofac.Core;
using BasicProject.Application;
using BasicProject.Application.Contracts;
using BasicProject.Application.Contracts.Users;
using BasicProject.Application.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Web;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement.Web;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TenantManagement.Web;

namespace BasicAspNetCoreApplication
{
    [DependsOn(typeof(AbpAspNetCoreMvcModule))]
    //[DependsOn(typeof(AbpAutofacModule))] // 在模块上添加依赖AbpAutofacModule
    [DependsOn(
        typeof(BasicProjectApplicationContractsModule), 
        typeof(BasicProjectApplicationModule)
        )]//Application+Contracts

    [DependsOn(
    )]
    public class BaseProjectWebModule : AbpModule
    {
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                //app.UseExceptionHandler("/Error");
                //// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSwagger();
            //app.UseAbpSwaggerUI(options =>
            //{
            //    options.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStore API");
            //});
            app.UseAbpSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "BasicProject API");
            });
            app.UseConfiguredEndpoints();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //常规理解是这样的
            //context.Services.AddSingleton<IUserAppService, UserAppService>();
            //context.Services.AddAbpSwaggerGen(
            context.Services.AddAbpSwaggerGen(
             options =>
             {
                 options.SwaggerDoc("v1", new OpenApiInfo { Title = "BasicProject API", Version = "v1" });
                 options.DocInclusionPredicate((docName, description) => true);
                 options.CustomSchemaIds(type => type.FullName);
             }
         );
            //配置option--初始化配置
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(BasicProjectApplicationModule).Assembly);
            });
        }
    }
}
