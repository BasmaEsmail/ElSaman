using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models
{
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.ToTable("Store");
            builder.HasKey(r => r.ID);
            builder.Property(r => r.ID).ValueGeneratedOnAdd();
            builder.Property(r => r.FromDate).IsRequired();
            builder.Property(r => r.ToDate).IsRequired();
            builder.Property(r => r.VendorID).IsRequired();
            builder.Property(r => r.NameEN).IsRequired().HasMaxLength(300);
            builder.Property(r => r.NameAR).IsRequired().HasMaxLength(300);
            builder.Property(r => r.ImageUrl).IsRequired().HasMaxLength(300);
            builder.Property(r => r.RegisterDate).HasDefaultValue(DateTime.Now);
            builder.Property(r => r.IsDeleted).HasDefaultValue(false);
        }
    }
}
