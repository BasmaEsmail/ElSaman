using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class FavConfiguration : IEntityTypeConfiguration<Fav>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Fav> builder)
        {
            builder.ToTable("Fav");
            builder.HasKey(F => F.ID);
            builder.Property(F => F.ID).ValueGeneratedOnAdd();
            builder.Property(F => F.UserID).HasMaxLength(450).IsRequired();
            builder.Property(F => F.ProductID).HasMaxLength(50).IsRequired();
        }
    }
}
