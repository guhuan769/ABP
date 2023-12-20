using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Elon.ForumABPExample.Data;
using Volo.Abp.DependencyInjection;

namespace Elon.ForumABPExample.EntityFrameworkCore;

public class EntityFrameworkCoreForumABPExampleDbSchemaMigrator
    : IForumABPExampleDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreForumABPExampleDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the ForumABPExampleDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<ForumABPExampleDbContext>()
            .Database
            .MigrateAsync();
    }
}
