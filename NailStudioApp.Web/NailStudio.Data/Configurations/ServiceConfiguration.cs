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
    public class ServiceConfiguration:IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(m => m.Duration)
                .IsRequired();

            builder.Property(m => m.ImageUrl)
                .IsRequired()
                .HasMaxLength(500);

           

            builder.HasData(this.SeedServices());
        }
        private List<Service> SeedServices()
        {
            List<Service> services = new List<Service>()
            {
                new Service()
                {
                    Id = 1,
                    Name = "Manicure",
                    Description = "A relaxing manicure session including nail shaping, cuticle care, and polish application.",
                    Price = 25.00m,
                    Duration = new TimeSpan(0, 45, 0), // 45 minutes
                    ImageUrl = "https://globalfashion.ru/images/blog/articles/oTLr0HS5G1IwBsDaZemFzRQ22QUCKzMIVCgKL6Iw.jpg"
                },
                new Service()
                {
                    Id = 2,
                    Name = "Pedicure",
                    Description = "A soothing pedicure with foot soak, exfoliation, nail shaping, and polish.",
                    Price = 35.00m,
                    Duration = new TimeSpan(1, 0, 0), // 1 hour
                    ImageUrl = "https://blog.globalfashion.pro/ua/articles/IIIexSx2c6IDfVtXwTellmCrXEJVZITNlQpoR4Vs.jpg"
                }
            };
            return services;
        }
    }
}
