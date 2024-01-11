using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace MicroClassroom.Enterprise.EntityFrameworkCore.ModelConfigurations;

internal class CourseItemDbMapping : IEntityTypeConfiguration<CourseItem>
{
    public void Configure(EntityTypeBuilder<CourseItem> builder)
    {
        // Primary Key
        builder.HasKey(m => m.Id).HasName("pk_course_item_id");

        // Table & Column Mappings
        builder.ToTable("CourseItem");

        builder.Property(t => t.Id)
            .ValueGeneratedNever()
            .IsRequired()
            .HasColumnName("Id")
            .HasComment("主键标识");

        builder.Property(t => t.CourseId).IsRequired()
            .HasColumnName("CourseId").HasComment("课程id");
        builder.Property(t => t.Title).IsRequired().HasMaxLength(200)
            .HasColumnName("Name").HasComment("目录名");
        builder.Property(t => t.Order).IsRequired()
            .HasColumnName("Order").HasComment("排序");
        builder.Property(t => t.Duration)
            .HasColumnName("Duration").HasComment("视频时长");
        builder.Property(t => t.Video).IsRequired().HasMaxLength(500)
            .HasColumnName("Video").HasComment("视频地址");
        builder.Property(t => t.StartAt)
            .HasColumnName("StartAt").HasComment("开始时间");
        builder.Property(t => t.EndAt)
            .HasColumnName("EndAt").HasComment("结束时间");
        builder.Property(t => t.TenantId)
            .HasColumnName("TenantId").HasComment("租户id");

        builder.ConfigureByConvention();
        builder.ApplyObjectExtensionMappings();
    }
}
