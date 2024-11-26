using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NailStudioApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Data.Configurations
{
    public class AppointmentServiceConfiguration:IEntityTypeConfiguration<AppointmentService>
    {
        public void Configure(EntityTypeBuilder<AppointmentService> builder)
        {
            builder.HasKey(asv => new { asv.AppointmentId, asv.ServiceId });

            builder
                .HasOne(asv => asv.Appointment)
                .WithMany(a => a.AppointmentServices)
                .HasForeignKey(asv => asv.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(asv => asv.Service)
                .WithMany(s => s.AppointmentServices)
                .HasForeignKey(asv => asv.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(this.SeedAppointmentServices());
        }

        private List<AppointmentService> SeedAppointmentServices()
        {
            return new List<AppointmentService>
            {
                new AppointmentService
                {
                    AppointmentId = 1,
                    ServiceId = 1 // Manicure
                },
                new AppointmentService
                {
                    AppointmentId = 2,
                    ServiceId = 2 // Pedicure
                }
            };
        }
    }
}
