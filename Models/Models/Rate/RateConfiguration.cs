using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models
{
    public class RateConfiguration : IEntityTypeConfiguration<Rate>
    {
        public void Configure(EntityTypeBuilder<Rate> builder)
        {
            builder.ToTable("Rate");
            builder.HasKey(R => R.ID);
            builder.Property(R => R.ID).ValueGeneratedOnAdd();
            builder.Property(R => R.Comment).HasMaxLength(450);
            builder.Property(R => R.RatingValue).IsRequired();
            builder.Property(R => R.ProductID).IsRequired();
            builder.Property(R => R.UserID).IsRequired();
            builder.Property(R => R.IsDeleted).IsRequired();


        }
    }
}
