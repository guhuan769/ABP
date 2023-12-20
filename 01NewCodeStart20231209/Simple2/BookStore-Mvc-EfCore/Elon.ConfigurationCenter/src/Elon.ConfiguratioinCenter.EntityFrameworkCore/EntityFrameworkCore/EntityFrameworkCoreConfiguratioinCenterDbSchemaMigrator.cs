using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Elon.ConfiguratioinCenter.Data;
using Volo.Abp.DependencyInjection;

namespace Elon.ConfiguratioinCenter.EntityFrameworkCore;

public class EntityFrameworkCoreConfiguratioinCenterDbSchemaMigrator
    : IConfiguratioinCenterDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreConfiguratioinCenterDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the ConfiguratioinCenterDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<ConfiguratioinCenterDbContext>()
            .Database
            .MigrateAsync();
    }
}
