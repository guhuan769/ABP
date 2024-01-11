using Volo.Abp;
using Volo.Abp.MongoDB;

namespace ManagementPlatform.Production.MongoDB;

public static class ProductionMongoDbContextExtensions
{
    public static void ConfigureProduction(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
