﻿using Acme.BookStore.Web;
using Autofac.Core;
using BasicProject.Application;
using BasicProject.Application.Contracts;
using BasicProject.Application.Contracts.Users;
using BasicProject.Application.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.CodeAnalysis.Options;
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
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "BasicProject.Web");
            });
            app.UseConfiguredEndpoints();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //常规理解是这样的
            //context.Services.AddSingleton<IUserAppService, UserAppService>();
            //context.Services.AddAbpSwaggerGen(

            //context.Services.AddEndpointsApiExplorer();
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
             }
         );
            //常规情况下，开发需要添加控制器---Action，但是也不干啥，然后企业去调用服务
            //有了 ABP的Auto API，直接自动编程了WEBAPI 省事
            //配置option--初始化配置  auto api
            base.Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(BasicProjectApplicationModule).Assembly);
            });
        }
    }
}
