using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace MicroClassroom.Customer.EntityFrameworkCore;

[ConnectionStringName(CustomerDbProperties.ConnectionStringName)]
public interface ICustomerDbContext : IEfCoreDbContext
{
    DbSet<Comment> Comments { get; }

    DbSet<Purchase> Purchase { get; }
}
