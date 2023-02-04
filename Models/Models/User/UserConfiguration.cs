using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(u => u.Id);
            builder.Property(U => U.NameEN).HasMaxLength(200).IsRequired();
            builder.Property(U => U.ImageUrl).HasMaxLength(200).IsRequired();
            builder.Property(U => U.NameAR).HasMaxLength(200).IsRequired();
            builder.Property(U => U.registerDate).HasDefaultValue(DateTime.Now);
            builder.Property(U => U.IsDeleted).HasDefaultValue(false);
        }
    }
}
