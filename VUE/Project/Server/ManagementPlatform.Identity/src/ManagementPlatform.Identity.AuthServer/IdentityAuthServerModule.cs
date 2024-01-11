using Localization.Resources.AbpUi;
using ManagementPlatform.Identity.EntityFrameworkCore;
using ManagementPlatform.Identity.Localization;
using ManagementPlatform.Identity.MultiTenancy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using System;
using System.IO;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Auditing;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.VirtualFileSystem;

namespace ManagementPlatform.Identity;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpCachingStackExchangeRedisModule),
    typeof(AbpDistributedLockingModule),
    typeof(AbpAccountWebOpenIddictModule),
    typeof(AbpAccountApplicationModule),
    typeof(AbpAccountHttpApiModule),
    typeof(AbpAspNetCoreMvcUiLeptonXLiteThemeModule),
    typeof(IdentityEntityFrameworkCoreModule),
    typeof(AbpAspNetCoreSerilogModule)
    )]
public class IdentityAuthServerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = MultiTenancyConsts.IsEnabled;
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<IdentityResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );

            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
        });

        Configure<AbpBundlingOptions>(options =>
        {
            options.StyleBundles.Configure(
                BasicThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/global-styles.css");
                }
            );
        });

        Configure<AbpAuditingOptions>(options =>
        {
            options.IsEnabled = false;
            //options.IsEnabledForGetRequests = true;
            //options.ApplicationName = "AuthServer";
        });

        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<IdentityDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}ManagementPlatform.Identity.Domain.Shared"));
                options.FileSets.ReplaceEmbeddedByPhysical<IdentityDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}ManagementPlatform.Identity.Domain"));
            });
        }

        //Configure<AppUrlOptions>(options =>
        //{
        //    options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
        //    options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"].Split(','));

        //    options.Applications["Angular"].RootUrl = configuration["App:ClientUrl"];
        //    options.Applications["Angular"].Urls[AccountUrlNames.PasswordReset] = "account/reset-password";
        //});

        Configure<AbpBackgroundJobOptions>(options =>
        {
            options.IsJobExecutionEnabled = false;
        });

        context.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration["Redis:Configuration"];
        });

        var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
        context.Services.AddDataProtection()
            .PersistKeysToStackExchangeRedis(redis, "ManagementPlatform-DataProtection-Keys");

        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(
                        configuration["App:CorsOrigins"]
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.RemovePostFix("/"))
                            .ToArray()
                    )
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
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

        app.UseAbpRequestLocalization();

        if (!env.IsDevelopment())
        {
            app.UseErrorPage();
        }

        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseUnitOfWork();
        app.UseIdentityServer();
        app.UseAuthorization();
        //app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}
//{
//    public override void PreConfigureServices(ServiceConfigurationContext context)
//    {
//        var hostingEnvironment = context.Services.GetHostingEnvironment();
//        var configuration = context.Services.GetConfiguration();

//        PreConfigure<OpenIddictBuilder>(builder =>
//        {
//            builder.AddValidation(options =>
//            {
//                options.AddAudiences("Identity");
//                options.UseLocalServer();
//                options.UseAspNetCore();
//            });
//        });

//        if (!hostingEnvironment.IsDevelopment())
//        {
//            PreConfigure<AbpOpenIddictAspNetCoreOptions>(options =>
//            {
//                options.AddDevelopmentEncryptionAndSigningCertificate = false;
//            });

//            PreConfigure<OpenIddictServerBuilder>(serverBuilder =>
//            {
//                serverBuilder.AddProductionEncryptionAndSigningCertificate("openiddict.pfx", "31a7da77-eebb-484c-bbbb-c7a738ed8c87");
//            });
//        }
//    }

//    public override void ConfigureServices(ServiceConfigurationContext context)
//    {
//        var hostingEnvironment = context.Services.GetHostingEnvironment();
//        var configuration = context.Services.GetConfiguration();

//        Configure<AbpLocalizationOptions>(options =>
//        {
//            options.Resources
//                .Get<IdentityResource>()
//                .AddBaseTypes(
//                    typeof(AbpUiResource),
//                    typeof(AccountResource)
//                );

//            options.Languages.Add(new LanguageInfo("ar", "ar", "العربية"));
//            options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
//            options.Languages.Add(new LanguageInfo("en", "en", "English"));
//            options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (UK)"));
//            options.Languages.Add(new LanguageInfo("fi", "fi", "Finnish"));
//            options.Languages.Add(new LanguageInfo("fr", "fr", "Français"));
//            options.Languages.Add(new LanguageInfo("hi", "hi", "Hindi", "in"));
//            options.Languages.Add(new LanguageInfo("is", "is", "Icelandic", "is"));
//            options.Languages.Add(new LanguageInfo("it", "it", "Italiano", "it"));
//            options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
//            options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
//            options.Languages.Add(new LanguageInfo("ro-RO", "ro-RO", "Română"));
//            options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
//            options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak"));
//            options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
//            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
//            options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
//            options.Languages.Add(new LanguageInfo("de-DE", "de-DE", "Deutsch", "de"));
//            options.Languages.Add(new LanguageInfo("es", "es", "Español", "es"));

//        });

//        Configure<AbpBundlingOptions>(options =>
//        {
//            options.StyleBundles.Configure(
//                LeptonXLiteThemeBundles.Styles.Global,
//                bundle =>
//                {
//                    bundle.AddFiles("/global-styles.css");
//                }
//            );
//        });

//        Configure<AbpAuditingOptions>(options =>
//        {
//            options.IsEnabled = false;
//            //options.IsEnabledForGetRequests = true;
//            //options.ApplicationName = "AuthServer";
//        });

//        if (hostingEnvironment.IsDevelopment())
//        {
//            Configure<AbpVirtualFileSystemOptions>(options =>
//            {
//                options.FileSets.ReplaceEmbeddedByPhysical<IdentityDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}ManagementPlatform.Identity.Domain.Shared"));
//                options.FileSets.ReplaceEmbeddedByPhysical<IdentityDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}ManagementPlatform.Identity.Domain"));
//            });
//        }

//        Configure<AppUrlOptions>(options =>
//        {
//            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
//            options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"]?.Split(',') ?? Array.Empty<string>());

//            options.Applications["Angular"].RootUrl = configuration["App:ClientUrl"];
//            options.Applications["Angular"].Urls[AccountUrlNames.PasswordReset] = "account/reset-password";
//        });

//        Configure<AbpBackgroundJobOptions>(options =>
//        {
//            options.IsJobExecutionEnabled = false;
//        });

//        context.Services.AddStackExchangeRedisCache(options =>
//        {
//            options.Configuration = configuration["Redis:Configuration"];
//        });

//        var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
//        context.Services.AddDataProtection()
//            .PersistKeysToStackExchangeRedis(redis, "ManagementPlatform-DataProtection-Keys");

//        context.Services.AddSingleton<IDistributedLockProvider>(sp =>
//        {
//            var connection = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]!);
//            return new RedisDistributedSynchronizationProvider(connection.GetDatabase());
//        });

//        context.Services.AddCors(options =>
//        {
//            options.AddDefaultPolicy(builder =>
//            {
//                builder
//                    .WithOrigins(
//                        configuration["App:CorsOrigins"]
//                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
//                            .Select(o => o.RemovePostFix("/"))
//                            .ToArray()
//                    )
//                    .WithAbpExposedHeaders()
//                    .SetIsOriginAllowedToAllowWildcardSubdomains()
//                    .AllowAnyHeader()
//                    .AllowAnyMethod()
//                    .AllowCredentials();
//            });
//        });

//        //context.Services.Configure<AbpClaimsPrincipalFactoryOptions>(options =>
//        //{
//        //    options.IsDynamicClaimsEnabled = true;
//        //});
//    }

//    public override void OnApplicationInitialization(ApplicationInitializationContext context)
//    {
//        var app = context.GetApplicationBuilder();
//        var env = context.GetEnvironment();

//        if (env.IsDevelopment())
//        {
//            app.UseDeveloperExceptionPage();
//        }

//        app.UseAbpRequestLocalization();

//        if (!env.IsDevelopment())
//        {
//            app.UseErrorPage();
//        }

//        app.UseCorrelationId();
//        app.UseStaticFiles();
//        app.UseRouting();
//        app.UseCors();
//        app.UseAuthentication();

//        if (MultiTenancyConsts.IsEnabled)
//        {
//            app.UseMultiTenancy();
//        }

//        app.UseUnitOfWork();
//        app.UseIdentityServer();
//        app.UseAuthorization();
//        //app.UseAuditing();
//        app.UseAbpSerilogEnrichers();
//        app.UseConfiguredEndpoints();
//    }
//}
