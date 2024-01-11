using MicroClassroom.Customer.EntityFrameworkCore.ModelConfigurations;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace MicroClassroom.Customer.EntityFrameworkCore;

public static class CustomerDbContextModelCreatingExtensions
{
    public static void ConfigureCustomer(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.ApplyConfiguration(new CommentDbMapping())
            .ApplyConfiguration(new PurchaseDbMapping());
    }
}
