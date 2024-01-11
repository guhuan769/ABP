using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace ManagementPlatform.Company.EntityFrameworkCore.ModelConfigurations
{
    public class CompanyDbMapping : IEntityTypeConfiguration<Company.Company>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Company.Company> builder)
        {
            //Primary Key
            builder.HasKey(t => t.Id).HasName("pk_company_id");

            builder.ToTable("Company");

            builder.Property(t => t.Id)
           .ValueGeneratedNever()
           .IsRequired()
           .HasColumnName("Id")
           .HasComment("主键标识");

            builder.HasMany(c => c.CompanyItems).WithOne().HasForeignKey(t => t.CompanyId)
                .HasConstraintName("fk_company_item_id").IsRequired();

            builder.ConfigureByConvention();
            builder.ApplyObjectExtensionMappings();
        }
        
    }
}
