using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace MicroClassroom.Enterprise.EntityFrameworkCore.ModelConfigurations;

internal class TeacherDbMapping : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        // Primary Key
        builder.HasKey(m => m.Id).HasName("pk_teacher_id");

        // Table & Column Mappings
        builder.ToTable("Teacher");

        builder.Property(t => t.Id)
            .ValueGeneratedNever()
            .IsRequired()
            .HasColumnName("Id")
            .HasComment("主键标识");

        builder.Property(t => t.MechanismId).IsRequired()
            .HasColumnName("MechanismId").HasComment("机构");
        builder.Property(t => t.Name).IsRequired().HasMaxLength(200)
            .HasColumnName("Name").HasComment("名称");
        builder.Property(t => t.Image).IsRequired().HasMaxLength(500)
            .HasColumnName("Image").HasComment("图像");
        builder.Property(t => t.Introduce).HasMaxLength(500)
            .HasColumnName("Introduce").HasComment("介绍");
        builder.Property(t => t.TenantId)
            .HasColumnName("TenantId").HasComment("租户id");

        builder.ConfigureByConvention();
        builder.ApplyObjectExtensionMappings();
    }
}
