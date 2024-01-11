using Volo.Abp.Account;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace MicroClassroom.Identity;

public static class IdentityDtoExtensions
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
            ObjectExtensionManager.Instance
                .AddOrUpdateProperty<int?>(
                    new[] {
                        typeof(IdentityUserDto),
                        typeof(IdentityUserCreateDto),
                        typeof(IdentityUserUpdateDto),
                        typeof(ProfileDto),
                        typeof(UpdateProfileDto)
                    }, "Gender"
                )
                .AddOrUpdateProperty<string>(
                    new[] {
                        typeof(IdentityUserDto),
                        typeof(IdentityUserCreateDto),
                        typeof(IdentityUserUpdateDto),
                        typeof(ProfileDto),
                        typeof(UpdateProfileDto)
                    }, "Avatar"
                );
        });
    }
}
