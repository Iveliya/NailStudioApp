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

            builder.Property(sm => sm.Specialization)
                   .IsRequired()
                   .HasMaxLength(100); 

            builder.HasMany(sm => sm.Appointments)
                   .WithOne(a => a.StaffMember)
                   .HasForeignKey(a => a.StaffMemberId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasData(this.SeedStaffMembers());
        }

        private List<StaffMember> SeedStaffMembers()
        {
            List<StaffMember> staffMembers = new List<StaffMember>()
            {
                new StaffMember()
                {
                    Id = Guid.NewGuid(),
                    Name = "John Doe",
                    Specialization = "Manicure"
                },
                new StaffMember()
                {
                    Id = Guid.NewGuid(),
                    Name = "Jane Smith",
                    Specialization = "Pedicure"
                }
            };

            return staffMembers;
        }
    }
}
