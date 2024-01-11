using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Elon.BasicInfo;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(BasicInfoHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class BasicInfoConsoleApiClientModule : AbpModule
{

}
