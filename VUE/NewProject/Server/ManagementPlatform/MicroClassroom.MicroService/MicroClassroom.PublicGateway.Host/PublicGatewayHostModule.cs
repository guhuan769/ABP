using MicroClassroom.Customer;
using MicroClassroom.Enterprise;
using MicroClassroom.Shared;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using StackExchange.Redis;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace MicroClassroom.PublicGateway.Host;


[DependsOn(
typeof(AbpAutofacModule),
typeof(EnterpriseHttpApiModule),
typeof(CustomerHttpApiModule),
typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
typeof(AbpMultiTenancyModule),
typeof(AbpTenantManagementEntityFrameworkCoreModule)
)]
public class PublicGatewayHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        // 启用多租户
        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = MicroClassroomConsts.IsMultiTenancyEnabled;
        });

        // Identity Server认证
        context.Services.AddAuthentication("Bearer")
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = configuration["AuthServer:Authority"];
                options.ApiName = configuration["AuthServer:ApiName"];
                options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
            });

        context.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Gateway API", Version = "v1" });
            options.DocInclusionPredicate((docName, description) => true);
            options.CustomSchemaIds(type => type.FullName);
        });

        // 定义网关资源
        context.Services.AddOcelot(context.Services.GetConfiguration());

        // Redis
        context.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration["Redis:Configuration"];
        });

        var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
        context.Services.AddDataProtection()
            .PersistKeysToStackExchangeRedis(redis, "MicroClassroom-DataProtection-Keys");
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();

        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseAbpClaimsMap();
        if (MicroClassroomConsts.IsMultiTenancyEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Public Gateway API");
        });

        // Abp vNext 接口 主管道的中间件不会被执行
        app.MapWhen(
            ctx => ctx.Request.Path.ToString().StartsWith("/api/abp/") ||
                   ctx.Request.Path.ToString().StartsWith("/Abp/"),
            app2 =>
            {
                app2.UseRouting();
                app2.UseConfiguredEndpoints();
            }
        );

        app.UseOcelot().Wait();
    }
}

