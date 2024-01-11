using Volo.Abp;
using Volo.Abp.MongoDB;

namespace ManagementPlatform.Company.MongoDB;

public static class CompanyMongoDbContextExtensions
{
    public static void ConfigureCompany(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
