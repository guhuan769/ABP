using Volo.Abp;
using Volo.Abp.MongoDB;

namespace MicroClassroom.Enterprise.MongoDB;

public static class EnterpriseMongoDbContextExtensions
{
    public static void ConfigureEnterprise(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
