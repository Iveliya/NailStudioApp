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
    public class UserServiceConfiguration : IEntityTypeConfiguration<UserService>
    {
        public void Configure(EntityTypeBuilder<UserService> builder)
        {
            builder.HasKey(us => new { us.UserId, us.ServiceId });

            builder.HasOne(us => us.User)
                   .WithMany(u => u.UserServices)
                   .HasForeignKey(us => us.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(us => us.Service)
                   .WithMany(s => s.UserServices)
                   .HasForeignKey(us => us.ServiceId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(us => us.IsDeleted)
                   .IsRequired();

            builder.HasQueryFilter(us => !us.IsDeleted);


            //builder.HasData(SeedUserServices());
        }

        //private List<UserService> SeedUserServices()
        //{
        //    return new List<UserService>
        //    {
        //        new UserService
        //        {
        //            UserId = Guid.Parse("YOUR_USER_GUID_1"),
        //            ServiceId = Guid.Parse("YOUR_SERVICE_GUID_1")
        //        },
        //        new UserService
        //        {
        //            UserId = Guid.Parse("YOUR_USER_GUID_2"),
        //            ServiceId = Guid.Parse("YOUR_SERVICE_GUID_2")
        //        }
        //    };
        //}
    }
}
