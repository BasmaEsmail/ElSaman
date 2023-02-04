using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(r => r.ID);
            builder.Property(r => r.ID).ValueGeneratedOnAdd();
            builder.Property(r => r.ID).IsRequired();
            builder.Property(r => r.ImageUrl).IsRequired();
            builder.Property(r => r.Details).HasMaxLength(250);
            builder.Property(r => r.NameAR).HasMaxLength(30).IsRequired();
            builder.Property(r => r.NameEN).HasMaxLength(30).IsRequired();
            builder.Property(r => r.RegisterDate).HasDefaultValue(DateTime.Now);
        }
    }
}
