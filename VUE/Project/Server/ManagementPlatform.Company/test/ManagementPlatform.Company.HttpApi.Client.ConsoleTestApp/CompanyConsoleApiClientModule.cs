using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace ManagementPlatform.Company;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(CompanyHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class CompanyConsoleApiClientModule : AbpModule
{

}
