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
    public class ScheduleConfiguration:IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.DayOfWeek)
                .IsRequired();

            builder.Property(s => s.StartTime)
                .IsRequired();

            builder.Property(s => s.EndTime)
                .IsRequired();

            builder
                .HasOne(s => s.Employee) 
                .WithMany(e => e.Schedules)
                .HasForeignKey(s => s.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(this.SeedSchedules());
        }

        private List<Schedule> SeedSchedules()
        {
            return new List<Schedule>
            {
                new Schedule
                {
                    Id = 1,
                    EmployeeId = 1,
                    DayOfWeek = 1, // Понеделник
                    StartTime = new TimeSpan(9, 0, 0),
                    EndTime = new TimeSpan(17, 0, 0)
                },
                new Schedule
                {
                    Id = 2,
                    EmployeeId = 2,
                    DayOfWeek = 2, // Вторник
                    StartTime = new TimeSpan(10, 0, 0),
                    EndTime = new TimeSpan(18, 0, 0)
                }
            };
        }
    }
}
