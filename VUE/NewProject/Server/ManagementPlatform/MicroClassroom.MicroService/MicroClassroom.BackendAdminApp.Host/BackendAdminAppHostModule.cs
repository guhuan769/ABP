using MicroClassroom.Customer;
using MicroClassroom.Enterprise;
using MicroClassroom.Enterprise.Localization;
using MicroClassroom.Identity;
using MicroClassroom.Shared;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using Volo.Abp;
using Volo.Abp.AspNetCore.Authentication.OAuth;
using Volo.Abp.AspNetCore.Mvc.Client;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using Volo.Abp.Auditing;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.Minio;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Http.Client.IdentityModel.Web;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Web;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.Web;
using Volo.Abp.UI.Navigation;

namespace MicroClassroom.BackendAdminApp.Host;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpAspNetCoreMvcClientModule),
    typeof(AbpAspNetCoreAuthenticationOAuthModule),
    typeof(AbpHttpClientIdentityModelWebModule),
    typeof(AbpIdentityHttpApiClientModule),
    typeof(AbpIdentityWebModule),
    typeof(AbpTenantManagementHttpApiClientModule),
    typeof(AbpTenantManagementWebModule),
    typeof(AbpPermissionManagementHttpApiClientModule),
    typeof(AbpAspNetCoreMvcUiBasicThemeModule),
    typeof(AbpFeatureManagementHttpApiClientModule),
    typeof(EnterpriseHttpApiClientModule),
    typeof(EnterpriseHttpApiModule),
    typeof(IdentityHttpApiClientModule),
    typeof(IdentityHttpApiModule),
    typeof(CustomerHttpApiClientModule),
    typeof(CustomerHttpApiModule),
    typeof(AbpBlobStoringMinioModule)
    )]
public class BackendAdminAppHostModule : AbpModule
{

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(EnterpriseResource),
                typeof(EnterpriseDomainSharedModule).Assembly,
                typeof(EnterpriseApplicationContractsModule).Assembly
            );
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        Configure<AbpAuditingOptions>(options =>
        {
            options.IsEnabled = false;
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
        });

        // 启用多租户
        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = MicroClassroomConsts.IsMultiTenancyEnabled;
        });

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new BackendAdminAppMenuContributor(configuration));
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
            options.Scope.Add("BackendAdminGateway");
            options.Scope.Add("IdentityService");
            options.Scope.Add("EnterpriseService");
            options.Scope.Add("CustomerService");
        });

        context.Services.AddSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Backend Admin Application API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
            });

        // redis
        context.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration["Redis:Configuration"];
        });

        var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
        context.Services.AddDataProtection()
            .PersistKeysToStackExchangeRedis(redis, "MicroClassroom-DataProtection-Keys");

        // Minio
        Configure<AbpBlobStoringOptions>(options =>
        {
            options.Containers.ConfigureDefault(container =>
            {
                container.UseMinio(minio =>
                {
                    minio.EndPoint = "192.168.173.134:9000";
                    minio.AccessKey = "admin";
                    minio.SecretKey = "zhaoxi@2019";
                    minio.WithSSL = false;
                });
            });
        });

        // AutoMapper
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<BackendAdminAppAutoMapperProfile>();
        });
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
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend Admin Application API");
        });
        app.UseConfiguredEndpoints();
    }
}
