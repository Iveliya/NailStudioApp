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
    using NailStudio.Data.Models;
    public class TimeSlotConfiguration : IEntityTypeConfiguration<TimeSlot>
    {
        public void Configure(EntityTypeBuilder<TimeSlot> builder)
        {
            builder.HasKey(ts => ts.Id);

            builder.HasIndex(ts => new { ts.Date, ts.Time, ts.StaffMemberId })
                   .IsUnique();

            builder.Property(ts => ts.Date)
                   .IsRequired();

            builder.Property(ts => ts.Time)
                   .IsRequired();

            builder.Property(ts => ts.IsBooked)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.Property(ts => ts.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.HasOne(ts => ts.User)
       .WithMany(u => u.TimeSlots) 
       .HasForeignKey(ts => ts.UserId)
       .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(ts => ts.StaffMember)
               .WithMany(sm => sm.TimeSlots)
               .HasForeignKey(ts => ts.StaffMemberId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ts => ts.Service)
                   .WithMany(s => s.TimeSlots) 
                   .HasForeignKey(ts => ts.ServiceId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.ToTable("TimeSlots");
        }
    }
}
