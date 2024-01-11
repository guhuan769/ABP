using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Elon.BasicInfo.MongoDB;

public static class BasicInfoMongoDbContextExtensions
{
    public static void ConfigureBasicInfo(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
