using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models
{
    public class VendorMemberShipConfiguration : IEntityTypeConfiguration<VendorMemberShip>
    {
        public void Configure(EntityTypeBuilder<VendorMemberShip> builder)
        {
            builder.ToTable("Vendor_MemberShip");
            builder.HasKey(vm => new { vm.VendorID, vm.MemberShipID });
            builder.Property(vm => vm.VendorID).IsRequired();
            builder.Property(vm => vm.MemberShipID).IsRequired();
            builder.Property(vm => vm.NumberOfOrders);
            builder.Property(vm => vm.IsValid).HasDefaultValue(false);
        }
    }
}
