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
    using static Common.EntityValidationConstans.User;
    public class UserConfiguration:IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(UsernameMaxLength); 

            builder.Property(u => u.PasswordHash)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasMaxLength(EmailMaxLength);

            builder.Property(u => u.PhoneNumber)
                .HasMaxLength(PhoneMaxLength);

            builder.Property(u => u.Role)
                .IsRequired()
                .HasDefaultValue(DefaultRole);

            builder.Property(u => u.CreatedAt)
                .IsRequired();

            builder
                .HasOne(u => u.Client) 
                .WithOne(c => c.User)
                .HasForeignKey<Client>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(this.SeedUsers());
        }

        private List<User> SeedUsers()
        {
            return new List<User>
            {
                new User
                {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = "hashed_password",
                    Email = "admin@example.com",
                    PhoneNumber = "123456789",
                    Role = "Admin",
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    Id = 2,
                    Username = "client1",
                    PasswordHash = "hashed_password",
                    Email = "client1@example.com",
                    PhoneNumber = "987654321",
                    Role = "Client",
                    CreatedAt = DateTime.UtcNow
                }
            };
        }
    }
}
