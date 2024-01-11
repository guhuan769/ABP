using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace MicroClassroom.Enterprise.EntityFrameworkCore.ModelConfigurations;

internal class BannerDbMapping : IEntityTypeConfiguration<Banner>
{
    public void Configure(EntityTypeBuilder<Banner> builder)
    {
        // Primary Key
        builder.HasKey(m => m.Id).HasName("pk_banner_id");

        // Table & Column Mappings
        builder.ToTable("Banner");

        builder.Property(t => t.Id)
            .ValueGeneratedNever()
            .IsRequired()
            .HasColumnName("Id")
            .HasComment("主键标识");

        builder.Property(t => t.MechanismId).IsRequired()
            .HasColumnName("MechanismId").HasComment("机构");
        builder.Property(t => t.Title).IsRequired().HasMaxLength(200)
            .HasColumnName("Title").HasComment("标题");
        builder.Property(t => t.Image).HasMaxLength(500)
            .HasColumnName("Image").IsRequired().HasComment("图像");
        builder.Property(t => t.TenantId)
            .HasColumnName("TenantId").HasComment("租户id");

        builder.ConfigureByConvention();
        builder.ApplyObjectExtensionMappings();
    }
}
