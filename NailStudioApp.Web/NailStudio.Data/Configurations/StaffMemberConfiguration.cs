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
    public class StaffMemberConfiguration: IEntityTypeConfiguration<StaffMember>
    {
        public void Configure(EntityTypeBuilder<StaffMember> builder)
        {
            builder.HasKey(sm => sm.Id);

            builder.Property(sm => sm.Name)
                   .IsRequired()
                   .HasMaxLength(100); 

            builder.Property(sm => sm.Role)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(sm => sm.PhotoUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(sm => sm.IsDeleted)
                   .IsRequired();

            builder.HasMany(sm => sm.Appointments)
                   .WithOne(a => a.StaffMember)
                   .HasForeignKey(a => a.StaffMemberId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(sm => sm.Schedules)
                   .WithOne(s => s.StaffMember)
                   .HasForeignKey(s => s.StaffMemberId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(sm => !sm.IsDeleted);

            builder.HasData(this.SeedStaffMembers());
        }

        private List<StaffMember> SeedStaffMembers()
        {
            return new List<StaffMember>
             {
                 new StaffMember
                 {
                     Id = Guid.NewGuid(),
                     Name = "Anna Rose",
                     Role = "Nail Technician",
                     PhotoUrl="https://www.flagman.bg/news/2024/10/30/173027983811467.png",
                     IsDeleted = false
                 },
                 new StaffMember
                 {
                     Id = Guid.NewGuid(),
                     Name = "Jane Smith",
                     Role = "Manager",
                     PhotoUrl="https://visages.net/wp-content/uploads/2022/06/dsc_3716.jpg",
                     IsDeleted = false
                 }
             };
        }
    }
}
