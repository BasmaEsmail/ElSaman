using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DBContext: IdentityDbContext<User>
    {
        #region DbSets
        public DBContext()
        { }
        public DBContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Fav> Favs { get; set; } 
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<MemberShip> MemberShips { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<VendorMemberShip> VendorMemberShips { get; set; }




        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Configurations
            new ProductConfiguration().Configure(modelBuilder.Entity<Product>());
            new CategoryConfiguration().Configure(modelBuilder.Entity<Category>());
            new CartConfiguration().Configure(modelBuilder.Entity<Cart>());
            new FavConfiguration().Configure(modelBuilder.Entity<Fav>());
            new OrderConfiguration().Configure(modelBuilder.Entity<Order>());
            new OrderItemConfiguration().Configure(modelBuilder.Entity<OrderItem>());
            new RateConfiguration().Configure(modelBuilder.Entity<Rate>());
            new MemberShipConfiguration().Configure(modelBuilder.Entity<MemberShip>());
            new StoreConfiguration().Configure(modelBuilder.Entity<Store>());
            new VendorConfiguration().Configure(modelBuilder.Entity<Vendor>());
            new VendorMemberShipConfiguration().Configure(modelBuilder.Entity<VendorMemberShip>());



            // modelBuilder.RoleData();
            #endregion
            RelationMapper.Mapper(modelBuilder);
            base.OnModelCreating(modelBuilder);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.\SQLExpress;Initial Catalog=ElSaman;Integrated Security=true");
        }
    }
}
