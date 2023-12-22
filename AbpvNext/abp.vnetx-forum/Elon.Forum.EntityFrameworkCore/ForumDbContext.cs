using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Elon.Forum.Domain.Entities;
using Elon.Forum.EntityFrameworkCore.ModelConfigurations;
using Zhaoxi.Forum.Domain.Entities;

namespace Elon.Forum.EntityFrameworkCore;

//[ConnectionStringName("Default")]
public class ForumDbContext : AbpDbContext<ForumDbContext>
{
    public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new CategoryDbMapping())
            .ApplyConfiguration(new TopicDbMapping());
    }

    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<TopicEntity> Topic { get; set; }
}