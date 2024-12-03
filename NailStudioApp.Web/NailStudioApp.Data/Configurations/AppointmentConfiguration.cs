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
    using static Common.EntityValidationConstans.Appointment;
    public class AppointmentConfiguration:IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.AppointmentDate)
                .IsRequired();

            builder.Property(a => a.TotalPrice)
                .IsRequired()
                .HasColumnType(TotalPriceValidation); 

            builder.Property(a => a.Status)
                .IsRequired()
                .HasMaxLength(StatusMaxLength) 
                .HasDefaultValue(StatusDefaultValue);

            

            builder
                .HasOne(a => a.Employee)
                .WithMany(e => e.Appointments)
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(a => a.AppointmentServices) 
                .WithOne(asv => asv.Appointment)
                .HasForeignKey(asv => asv.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(this.SeedAppointments());
        }

        private List<Appointment> SeedAppointments()
        {
            return new List<Appointment>
            {
                new Appointment
                {
                    Id = 1,
                    ClientId = 1,
                    EmployeeId = 1,
                    AppointmentDate = DateTime.UtcNow.AddDays(1), // Tomorrow
                    TotalPrice = 50.00m,
                    Status = "Confirmed"
                },
                new Appointment
                {
                    Id = 2,
                    ClientId = 2,
                    EmployeeId = 2,
                    AppointmentDate = DateTime.UtcNow.AddDays(3), // Three days from now
                    TotalPrice = 75.00m,
                    Status = "Pending"
                }
            };
        }
    }
}
