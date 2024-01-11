using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace MicroClassroom.Customer.EntityFrameworkCore.ModelConfigurations;

internal class CommentDbMapping : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        // Primary Key
        builder.HasKey(m => m.Id).HasName("pk_comment_id");

        // Table & Column Mappings
        builder.ToTable("Comment");

        builder.Property(t => t.Id)
            .ValueGeneratedNever()
            .IsRequired()
            .HasColumnName("Id")
            .HasComment("主键标识");

        builder.Property(t => t.CourseId).IsRequired()
            .HasColumnName("CourseId").HasComment("课程id");
        builder.Property(t => t.UserId).IsRequired()
            .HasColumnName("UserId").HasComment("用户id");
        builder.Property(t => t.Content).IsRequired().HasMaxLength(500)
            .HasColumnName("Content").HasComment("评论内容");
        builder.Property(t => t.Star).IsRequired()
            .HasColumnName("Star").HasComment("评分");
        builder.Property(t => t.CreateAt)
            .HasColumnName("CreateAt").HasComment("评论时间");
        builder.Property(t => t.TenantId)
            .HasColumnName("TenantId").HasComment("租户id");

        builder.ConfigureByConvention();
        builder.ApplyObjectExtensionMappings();
    }
}
