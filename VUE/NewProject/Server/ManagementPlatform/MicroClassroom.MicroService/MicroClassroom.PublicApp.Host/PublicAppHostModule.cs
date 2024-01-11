using MicroClassroom.Customer;
using MicroClassroom.Enterprise;
using MicroClassroom.Shared;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using StackExchange.Redis;
using Volo.Abp;
using Volo.Abp.AspNetCore.Authentication.OAuth;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.Client;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using Volo.Abp.Auditing;
using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel.Web;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.UI.Navigation;

namespace MicroClassroom.PublicApp.Host;

[DependsOn(
typeof(AbpAutofacModule),
typeof(AbpAspNetCoreMvcClientModule),
typeof(AbpAspNetCoreAuthenticationOAuthModule),
typeof(AbpHttpClientIdentityModelWebModule),
typeof(AbpAspNetCoreMvcUiBasicThemeModule),
typeof(AbpAspNetCoreMultiTenancyModule),
typeof(EnterpriseHttpApiClientModule),
typeof(CustomerHttpApiClientModule)
)]
public class PublicAppHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Languages.Add(new LanguageInfo("en", "en", "English"));
        });

        // 启用多租户
        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = MicroClassroomConsts.IsMultiTenancyEnabled;
        });

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new PublicAppMenuContributor());
        });

        // Identity Server认证
        context.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = "Cookies";
            options.DefaultChallengeScheme = "oidc";
        })
        .AddCookie("Cookies", options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromDays(365);
        })
        .AddOpenIdConnect("oidc", options =>
        {
            options.Authority = configuration["AuthServer:Authority"];
            options.ClientId = configuration["AuthServer:ClientId"];
            options.ClientSecret = configuration["AuthServer:ClientSecret"];
            options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
            options.ResponseType = OpenIdConnectResponseType.CodeIdToken;
            options.SaveTokens = true;
            options.GetClaimsFromUserInfoEndpoint = true;
            options.Scope.Add("role");
            options.Scope.Add("email");
            options.Scope.Add("phone");
            options.Scope.Add("PublicGateway");
            options.Scope.Add("CustomerService");
            options.Scope.Add("EnterpriseService");
        });

        // redis
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

        if (MicroClassroomConsts.IsMultiTenancyEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseAbpRequestLocalization();
        app.UseAuthorization();
        app.UseConfiguredEndpoints();
    }
}
