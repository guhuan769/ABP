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
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.FeatureManagement;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Http.Client.IdentityModel.Web;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement.IdentityServer;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace MicroClassroom.BackendAdminGateway.Host;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(EnterpriseHttpApiModule),
    typeof(CustomerHttpApiModule),
    typeof(AbpIdentityHttpApiModule),
    typeof(AbpIdentityHttpApiClientModule),
    typeof(AbpEntityFrameworkCoreSqlServerModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpPermissionManagementHttpApiModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementDomainIdentityModule),
    typeof(AbpPermissionManagementDomainIdentityServerModule),
    typeof(AbpHttpClientIdentityModelWebModule),
    typeof(AbpTenantManagementApplicationContractsModule),
    typeof(AbpTenantManagementHttpApiModule),
    typeof(AbpTenantManagementHttpApiClientModule),
    typeof(AbpTenantManagementEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpFeatureManagementHttpApiModule),
    typeof(AbpAspNetCoreMvcUiMultiTenancyModule)
)]
public class BackendAdminGatewayHost : AbpModule
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
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "BackendAdminGateway API", Version = "v1" });
            options.DocInclusionPredicate((docName, description) => true);
            options.CustomSchemaIds(type => type.FullName);
        });

        // 定义网关资源
        context.Services.AddOcelot(context.Services.GetConfiguration());

        // 租户、身份、权限、配置内置模块数据库连接
        Configure<AbpDbContextOptions>(options =>
        {
            options.UseSqlServer();
        });

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
        app.UseAbpClaimsMap();

        if (MicroClassroomConsts.IsMultiTenancyEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "BackendAdminGateway API");
        });

        // Abp vNext 接口 主管道的中间件不会被执行
        app.MapWhen(
            ctx => ctx.Request.Path.ToString().StartsWith("/api/abp/") ||
                   ctx.Request.Path.ToString().StartsWith("/Abp/") ||
                   ctx.Request.Path.ToString().StartsWith("/api/permission-management/") ||
                   ctx.Request.Path.ToString().StartsWith("/api/feature-management/"),
            app2 =>
            {
                app2.UseRouting();
                app2.UseConfiguredEndpoints();
            }
        );

        app.UseOcelot().Wait();
    }
}
