using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Elon.Production.MongoDB;

public static class ProductionMongoDbContextExtensions
{
    public static void ConfigureProduction(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
