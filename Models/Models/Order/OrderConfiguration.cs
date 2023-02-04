using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(O => O.ID);
            builder.Property(O => O.ID).ValueGeneratedOnAdd();
            builder.Property(O => O.Date).IsRequired();
            builder.Property(O => O.IsDeleted).IsRequired();
            builder.Property(O => O.UserId).IsRequired();
        }
    }
}
