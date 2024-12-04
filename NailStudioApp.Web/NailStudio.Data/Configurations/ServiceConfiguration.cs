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
                   .HasMaxLength(500); 

            builder.Property(s => s.Description)
                   .IsRequired()
                   .HasMaxLength(500); 

            builder.HasMany(s => s.Appointments)
                   .WithOne(a => a.Service)
                   .HasForeignKey(a => a.ServiceId)
                   .OnDelete(DeleteBehavior.Restrict); 

            builder.HasMany(s => s.UserServices)
                   .WithOne(us => us.Service)
                   .HasForeignKey(us => us.ServiceId)
                   .OnDelete(DeleteBehavior.Cascade); 

           
            builder.HasData(this.SeedServices());
        }

        private List<Service> SeedServices()
        {
            List<Service> services = new List<Service>()
            {
                new Service()
                {
                    Id = Guid.NewGuid(),
                    Name = "Manicure",
                    Description = "A relaxing manicure session including nail shaping, cuticle care, and polish application.",
                    Price = 25.00m,
                    DurationInMinutes = 45, // 45 minutes
                    ImageUrl = "https://globalfashion.ru/images/blog/articles/oTLr0HS5G1IwBsDaZemFzRQ22QUCKzMIVCgKL6Iw.jpg"
                },
                new Service()
                {
                    Id = Guid.NewGuid(),
                    Name = "Pedicure",
                    Description = "A soothing pedicure with foot soak, exfoliation, nail shaping, and polish.",
                    Price = 35.00m,
                    DurationInMinutes = 60, // 1 hour
                    ImageUrl = "https://blog.globalfashion.pro/ua/articles/IIIexSx2c6IDfVtXwTellmCrXEJVZITNlQpoR4Vs.jpg"
                }
            };

            return services;
        }

      
    }
}
