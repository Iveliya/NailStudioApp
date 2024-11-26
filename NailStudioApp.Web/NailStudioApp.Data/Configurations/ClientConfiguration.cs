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
    using static Common.EntityValidationConstans.Client;
    public class ClientConfiguration:IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(FirstNameMaxLength); 

            builder.Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(LastNameMaxLength); 

            builder.Property(c => c.PreferredTime)
                .IsRequired(PreferredTimeValidation); 

            builder
                .HasOne(c => c.User) 
                .WithOne(u => u.Client)
                .HasForeignKey<Client>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(c => c.Appointments)
                .WithOne(a => a.Client)
                .HasForeignKey(a => a.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(c => c.Reviews)
                .WithOne(r => r.Client)
                .HasForeignKey(r => r.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(this.SeedClients());
        }

        private List<Client> SeedClients()
        {
            return new List<Client>
            {
                new Client
                {
                    Id = 1,
                    UserId = 1,
                    FirstName = "Alice",
                    LastName = "Johnson",
                    PreferredTime = new TimeSpan(14, 0, 0) // 2:00 PM
                },
                new Client
                {
                    Id = 2,
                    UserId = 2,
                    FirstName = "Bob",
                    LastName = "Smith",
                    PreferredTime = new TimeSpan(10, 0, 0) // 10:00 AM
                }
            };
        }
    }
}
