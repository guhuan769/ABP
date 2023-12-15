using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.Http.Client;
using Elon.DashboardCenter.Application.Contracts;
using System;

namespace Acme.BookStore;

[DependsOn(
    typeof(BookStoreApplicationContractsModule),
    typeof(AbpAccountHttpApiClientModule),
    typeof(AbpIdentityHttpApiClientModule),
    typeof(AbpPermissionManagementHttpApiClientModule),
    typeof(AbpTenantManagementHttpApiClientModule),
    typeof(AbpFeatureManagementHttpApiClientModule),
    typeof(AbpSettingManagementHttpApiClientModule)
)]
[DependsOn(typeof(AbpHttpClientModule))] //动态API模块
[DependsOn(typeof(DashboardCenterApplicationContractsModule))] //会引用抽象
public class BookStoreHttpApiClientModule : AbpModule
{
    public const string RemoteServiceName = "Default";

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(BookStoreApplicationContractsModule).Assembly,
            RemoteServiceName
        );

        context.Services.AddHttpClientProxies(
            typeof(DashboardCenterApplicationContractsModule).Assembly,"Dashboard",
            asDefaultServices:false  // 是指这个抽象，可以  false 这个抽象会优先调用本地实现的为准 怎么样即本地又远程？
            );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<BookStoreHttpApiClientModule>();
        });
    }
}
