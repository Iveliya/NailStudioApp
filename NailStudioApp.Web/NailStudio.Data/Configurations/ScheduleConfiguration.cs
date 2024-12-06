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
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.StartTime)
                   .IsRequired();

            builder.Property(s => s.EndTime)
                   .IsRequired();

            builder.Property(s => s.IsAvailable)
                   .IsRequired();

            builder.Property(s => s.IsDeleted)
                   .IsRequired();

            builder.HasOne(s => s.StaffMember)
                   .WithMany(sm => sm.Schedules)
                   .HasForeignKey(s => s.StaffMemberId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(s => !s.IsDeleted);

            //builder.HasData(SeedSchedules());
        }

        //private List<Schedule> SeedSchedules()
        //{
        //    return new List<Schedule>
        //     {
        //         new Schedule
        //         {
        //             Id = Guid.NewGuid(),
        //             StartTime = new DateTime(2024, 1, 1, 9, 0, 0),
        //             EndTime = new DateTime(2024, 1, 1, 17, 0, 0),
        //             StaffMemberId = Guid.Parse("YOUR_STAFF_MEMBER_GUID"),
        //             IsAvailable = true,
        //             IsDeleted = false
        //         }
        //     };
        //}
    }
}
