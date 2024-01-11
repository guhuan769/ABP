using Microsoft.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace MicroClassroom.Identity.EntityFrameworkCore;

public static class IdentityEfCoreEntityExtensionMappings
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        IdentityGlobalFeatureConfigurator.Configure();
        IdentityModuleExtensionConfigurator.Configure();

        OneTimeRunner.Run(() =>
        {
            ObjectExtensionManager.Instance
            .MapEfCoreProperty<IdentityUser, int?>(
                "Gender",
                (entityBuilder, propertyBuilder) =>
                {
                    propertyBuilder.HasMaxLength(1);
                }
            )
            .MapEfCoreProperty<IdentityUser, string>(
                "Avatar",
                (entityBuilder, propertyBuilder) =>
                {
                    propertyBuilder.HasMaxLength(200);
                }
            );
        });
    }
}
