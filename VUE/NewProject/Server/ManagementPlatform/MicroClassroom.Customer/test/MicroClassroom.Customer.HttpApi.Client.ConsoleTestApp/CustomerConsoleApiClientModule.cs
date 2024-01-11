using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace MicroClassroom.Customer;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(CustomerHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class CustomerConsoleApiClientModule : AbpModule
{

}
