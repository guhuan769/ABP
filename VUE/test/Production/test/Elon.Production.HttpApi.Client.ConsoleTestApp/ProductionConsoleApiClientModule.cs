using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Elon.Production;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ProductionHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class ProductionConsoleApiClientModule : AbpModule
{

}
