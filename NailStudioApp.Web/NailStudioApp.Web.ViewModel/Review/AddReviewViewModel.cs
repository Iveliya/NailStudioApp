using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Web.ViewModel.Review
{
    public class AddReviewViewModel
    {
        [Required(ErrorMessage = "Review content cannot be empty.")]
        [StringLength(1000, ErrorMessage = "Review content cannot exceed 1000 characters.")]
        public string Content { get; set; } = string.Empty;

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        public Guid UserId { get; set; }
    }
}
