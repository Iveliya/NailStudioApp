﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NailStudioApp.Data.Models;

namespace NailStudioApp.Data.Configurations
{
    using static Common.EntityValidationConstans.Service;
    public class ServiceConfiguration:IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            builder.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(DescriptionMaxLength);

            builder.Property(m => m.Price)
                .IsRequired()
                .HasColumnType(PriceValidation);

            builder.Property(m => m.Duration)
                .IsRequired();

            builder.Property(m => m.ImageUrl) 
                .IsRequired() 
                .HasMaxLength(UrlMaxLength); 

            builder
                .HasMany(s => s.AppointmentServices)
                .WithOne(asv => asv.Service)
                .HasForeignKey(asv => asv.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);

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
                    ImageUrl = "https://globalfashion.ru/images/blog/articles/oTLr0HS5G1IwBsDaZemFzRQ22QUCKzMIVCgKL6Iw.jpg", 
                    AppointmentServices = new List<AppointmentService>()
                },
                new Service()
                {
                    Id = 2,
                    Name = "Pedicure",
                    Description = "A soothing pedicure with foot soak, exfoliation, nail shaping, and polish.",
                    Price = 35.00m,
                    Duration = new TimeSpan(1, 0, 0), // 1 hour
                    ImageUrl = "https://blog.globalfashion.pro/ua/articles/IIIexSx2c6IDfVtXwTellmCrXEJVZITNlQpoR4Vs.jpg", 
                    AppointmentServices = new List<AppointmentService>()
                }
            };
            return services;
        }
    }                                                                          
}
