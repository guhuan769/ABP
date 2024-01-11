﻿using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace MicroClassroom.Customer.MongoDB;

[ConnectionStringName(CustomerDbProperties.ConnectionStringName)]
public interface ICustomerMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
