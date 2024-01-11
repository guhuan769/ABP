using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace MicroClassroom.Enterprise;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(EnterpriseHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class EnterpriseConsoleApiClientModule : AbpModule
{

}
