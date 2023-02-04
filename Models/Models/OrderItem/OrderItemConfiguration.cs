using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models
{
    public class OrderItemConfiguration: IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItem");
            builder.HasKey(OL => OL.ID);
            builder.Property(OL => OL.ID).ValueGeneratedOnAdd();
            builder.Property(OL => OL.Qty).IsRequired();
            builder.Property(OL => OL.ProductID).IsRequired();
            builder.Property(OL => OL.Price).IsRequired();
            builder.Property(OL => OL.OrderID).IsRequired();


        }
    }
}
