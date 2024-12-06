using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NailStudio.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudio.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.IsDeleted)
                   .IsRequired();

            builder.HasMany(u => u.Appointments)
                   .WithOne(a => a.User)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.UserServices)
                   .WithOne(us => us.User)
                   .HasForeignKey(us => us.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Reviews)
                   .WithOne(r => r.User)
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(u => !u.IsDeleted);


            //builder.HasData(SeedUsers());
        }

        //private List<User> SeedUsers()
        //{
        //    return new List<User>
        //    {
        //        new User
        //        {
        //            Id = Guid.NewGuid(),
        //            UserName = "admin@example.com",
        //            NormalizedUserName = "ADMIN@EXAMPLE.COM",  
        //            Email = "admin@example.com",
        //            NormalizedEmail = "ADMIN@EXAMPLE.COM",
        //            Name = "Admin User",
        //            PasswordHash = "AQAAAAEAACcQAAAAEM6F0E0wXwHgPbLfdB8HKQhUqvTOtLBo2AOPwMOOB54JkPLppm9h9+na0LwlsTocWQ==",
        //            SecurityStamp = Guid.NewGuid().ToString()
        //        },
        //        new User
        //        {
        //            Id = Guid.NewGuid(),
        //            UserName = "user@example.com",
        //            NormalizedUserName = "USER@EXAMPLE.COM",
        //            Email = "user@example.com",
        //            NormalizedEmail = "USER@EXAMPLE.COM",
        //            Name = "Regular User",
        //            PasswordHash = "AQAAAAEAACcQAAAAEM6F0E0wXwHgPbLfdB8HKQhUqvTOtLBo2AOPwMOOB54JkPLppm9h9+na0LwlsTocWQ==",
        //            SecurityStamp = Guid.NewGuid().ToString()
        //        }
        //    };
        //}
    }
}
