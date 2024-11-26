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
    using static Common.EntityValidationConstans.Review;
    public class ReviewConfiguration:IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Comment)
                .IsRequired()
                .HasMaxLength(CommentMaxLength); 

            builder.Property(r => r.Rating)
                .IsRequired()
                .HasDefaultValue(DefaultRating) 
                .HasColumnType("int"); 

            builder.Property(r => r.ReviewDate)
                .IsRequired();

            builder
                .HasOne(r => r.Client) 
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(this.SeedReviews());
        }

        private List<Review> SeedReviews()
        {
            return new List<Review>
            {
                new Review
                {
                    Id = 1,
                    ClientId = 1,
                    Comment = "Excellent service! Highly recommend.",
                    Rating = 5,
                    ReviewDate = DateTime.UtcNow
                },
                new Review
                {
                    Id = 2,
                    ClientId = 2,
                    Comment = "Good, but could be better with timing.",
                    Rating = 4,
                    ReviewDate = DateTime.UtcNow.AddDays(-1)
                }
            };
        }
    }
}
