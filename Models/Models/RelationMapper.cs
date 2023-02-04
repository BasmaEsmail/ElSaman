using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class RelationMapper
    {
        public static void Mapper(ModelBuilder modelBuilder)
        {
            /*Product - Category */
            modelBuilder.Entity<Product>().HasOne(r => r.Category)
                .WithMany(c => c.Products).HasForeignKey(r => r.CategoryID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            /*User - Fav */
            modelBuilder.Entity<Fav>()
               .HasOne(U => U.User)
               .WithMany(F => F.favs)
               .HasForeignKey(U => U.UserID)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();
            /*User - Cart*/
            modelBuilder.Entity<Cart>()
               .HasOne(U => U.User)
               .WithMany(C => C.Carts)
               .HasForeignKey(U => U.UserID)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();
            /* Vendor MemberShip*/
            modelBuilder.Entity<VendorMemberShip>()
                .HasOne(vm => vm.Vendor)
                .WithMany(vm => vm.VendorMemberShips)
                .HasForeignKey(vm => vm.VendorID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VendorMemberShip>()
               .HasOne(vm => vm.MemberShip)
               .WithMany(vm => vm.Vendor_MemberShips)
               .HasForeignKey(vm => vm.MemberShipID)
               .OnDelete(DeleteBehavior.Cascade);

            /* Store - Vendor*/
            modelBuilder.Entity<Store>()
               .HasOne(vm => vm.Vendor)
               .WithMany(vm => vm.Stores)
               .HasForeignKey(vm => vm.VendorID)
               .OnDelete(DeleteBehavior.Cascade);

            /*Order - Order List */
            modelBuilder.Entity<OrderItem>().HasOne(O => O.Order)
               .WithMany(O => O.OrderItems)
               .HasForeignKey(O => O.OrderID)
               .OnDelete(DeleteBehavior.Cascade);

            /*User - Rating */
            modelBuilder.Entity<Rate>().HasOne(r => r.User)
                .WithMany(u => u.Rates).HasForeignKey(r => r.UserID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            /*Product - Fav */
            modelBuilder.Entity<Fav>().HasOne(f => f.Product)
                .WithMany(r => r.Favs).HasForeignKey(f => f.ProductID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            /*Recipe - Cart */
            modelBuilder.Entity<Cart>().HasOne(c => c.Product)
                .WithMany(r => r.Carts).HasForeignKey(f => f.ProductID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            /*Product - Rating */
            modelBuilder.Entity<Rate>().HasOne(ra => ra.Product)
                .WithMany(r => r.Rates).HasForeignKey(ra => ra.ProductID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            /*user - Order*/
            modelBuilder.Entity<Order>().HasOne(o => o.User)
                .WithMany(r => r.Orders).HasForeignKey(o => o.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            //Product => orderItem
            modelBuilder.Entity<OrderItem>().HasOne(r => r.Product)
                .WithMany(c => c.OrderItems).HasForeignKey(r => r.ProductID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            /*Product - Store*/
            modelBuilder.Entity<Product>().HasOne(r => r.Store)
                .WithMany(c => c.Products).HasForeignKey(r => r.StoreID)
                .OnDelete(DeleteBehavior.Cascade);
            /*Vendoe - User*/
            modelBuilder.Entity<Vendor>().HasOne(r => r.User)
                .WithOne(c => c.Vendor).HasForeignKey<Vendor>(r => r.ID)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
