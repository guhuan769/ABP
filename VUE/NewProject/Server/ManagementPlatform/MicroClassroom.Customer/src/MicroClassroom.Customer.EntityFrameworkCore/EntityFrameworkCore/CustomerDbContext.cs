using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace MicroClassroom.Customer.EntityFrameworkCore;

[ConnectionStringName(CustomerDbProperties.ConnectionStringName)]
public class CustomerDbContext : AbpDbContext<CustomerDbContext>, ICustomerDbContext
{
    public DbSet<Comment> Comments { get; set; }

    public DbSet<Purchase> Purchase { get; set; }

    public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureCustomer();
    }
}
