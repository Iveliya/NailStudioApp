using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Web.ViewModel.Review
{
    public class IndexReviewViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Review content cannot be empty.")]
        [StringLength(1000, ErrorMessage = "Review content cannot exceed 1000 characters.")]
        public string Content { get; set; } = string.Empty;

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Review date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; } = string.Empty;
    }
}
