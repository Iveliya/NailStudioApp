using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NailStudio.Data.Models;

namespace NailStudio.Data.Configurations
{
    public class ReviewConfiguration:IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Content)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(r => r.Rating)
                   .IsRequired()
                   .HasDefaultValue(0);

            builder.Property(r => r.Date)
                   .IsRequired();

            builder.Property(r => r.IsDeleted)
                   .IsRequired();

            builder.HasOne(r => r.User)
                   .WithMany(u => u.Reviews)
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

           
            builder.HasQueryFilter(r => !r.IsDeleted);

        }
    }
}
