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
    public class AppointmentConfguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        { 
            builder.HasKey(a => a.Id);

            builder.Property(a => a.AppointmentDate)
                   .IsRequired();

            builder.Property(a => a.IsDeleted)
                   .IsRequired();

            builder.HasOne(a => a.User)
                   .WithMany(u => u.Appointments)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Service)
                   .WithMany(s => s.Appointments)
                   .HasForeignKey(a => a.ServiceId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.StaffMember)
                   .WithMany(sm => sm.Appointments)
                   .HasForeignKey(a => a.StaffMemberId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(a => !a.IsDeleted);

             }

        //private List<Appointment> SeedAppointments()
        //{
        //    List<Appointment> appointments = new List<Appointment>()
        //    {
        //        new Appointment
        //        {
        //            Id = Guid.NewGuid(),
        //            UserId = Guid.Parse("e243e865-b20d-4eab-bb6d-11b5c7316cc8"), // Example UserId
        //            ServiceId = Guid.Parse("a6b1cbbc-84f3-4c6a-a885-77ff3c1bbddd"), // Example ServiceId
        //            AppointmentDate = DateTime.Now.AddDays(5),
        //            StaffMemberId = Guid.Parse("f316d54b-9724-4b2a-9c8a-dc9f509c9358") // Example StaffMemberId
        //        },
        //        new Appointment
        //        {
        //            Id = Guid.NewGuid(),
        //            UserId = Guid.Parse("e243e865-b20d-4eab-bb6d-11b5c7316cc8"), // Example UserId
        //            ServiceId = Guid.Parse("a6b1cbbc-84f3-4c6a-a885-77ff3c1bbddd"), // Example ServiceId
        //            AppointmentDate = DateTime.Now.AddDays(10),
        //            StaffMemberId = null // No staff member assigned
        //        }
        //    };

        //    return appointments;
        //}
    }
}
