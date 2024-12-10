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
    public class ServiceConfiguration: IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(s => s.Price)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)"); 

            builder.Property(s => s.DurationInMinutes)
                   .IsRequired();

            builder.Property(s => s.ImageUrl)
                   .IsRequired()
                   .HasMaxLength(200); 

            builder.Property(s => s.Description)
                   .IsRequired()
                   .HasMaxLength(500); 

            builder.Property(s => s.IsDeleted)
                   .IsRequired();

            builder.HasMany(s => s.Appointments)
                   .WithOne(a => a.Service)
                   .HasForeignKey(a => a.ServiceId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.UserServices)
                   .WithOne(us => us.Service)
                   .HasForeignKey(us => us.ServiceId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.TimeSlots) 
                   .WithOne(ts => ts.Service)
                   .HasForeignKey(ts => ts.ServiceId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasQueryFilter(s => !s.IsDeleted);

             builder.HasData(SeedServices()); }

        private List<Service> SeedServices()
        {
            return new List<Service>
             {
                 new Service
                 {
                     Id = Guid.NewGuid(),
                     Name = "Manicure",
                     Price = 30.00m,
                     DurationInMinutes = 60,
                     ImageUrl = "https://example.com/images/manicure.jpg",
                     Description = "A professional manicure service.",
                     IsDeleted = false
                 },
                 new Service
                 {
                     Id = Guid.NewGuid(),
                     Name = "Pedicure",
                     Price = 50.00m,
                     DurationInMinutes = 75,
                     ImageUrl = "https://example.com/images/pedicure.jpg",
                     Description = "A relaxing pedicure service.",
                     IsDeleted = false
                 }
             };
        }


    }
}
