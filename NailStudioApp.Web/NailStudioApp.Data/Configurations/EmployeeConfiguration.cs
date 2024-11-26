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
    using static Common.EntityValidationConstans.Employee;
    public class EmployeeConfiguration:IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(FirstNameMaxLength);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(LastNameMaxLength); 

            builder.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(PhoneMaxLength); 

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(EmailMaxLength);

            builder.Property(e => e.Specialization)
                .IsRequired()
                .HasMaxLength(SpezializationMaxLength); 

            builder.Property(e => e.HourlyRate)
                .IsRequired()
                .HasColumnType(HourlyRateValidation); 

            builder
                .HasMany(e => e.Appointments) 
                .WithOne(a => a.Employee)
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(e => e.Schedules) 
                .WithOne(s => s.Employee)
                .HasForeignKey(s => s.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(this.SeedEmployees());
        }

        private List<Employee> SeedEmployees()
        {
            return new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumber = "123456789",
                    Email = "john.doe@example.com",
                    Specialization = "Manicurist",
                    HourlyRate = 20.00m
                },
                new Employee
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    PhoneNumber = "987654321",
                    Email = "jane.smith@example.com",
                    Specialization = "Pedicurist",
                    HourlyRate = 22.50m
                }
            };
        }
    }
}
