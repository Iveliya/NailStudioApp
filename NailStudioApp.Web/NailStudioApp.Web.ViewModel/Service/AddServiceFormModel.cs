using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Web.ViewModel.Service
{
    public class AddServiceFormModel
    {
        [Required(ErrorMessage = "Service name is required.")]
        [StringLength(100, ErrorMessage = "Service name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Duration is required.")]
        [Range(1, 1440, ErrorMessage = "Duration must be between 1 minute and 1440 minutes (24 hours).")]
        public int DurationInMinutes { get; set; }

        [Required(ErrorMessage = "Image URL is required.")]
        [Url(ErrorMessage = "Invalid URL format.")]
        public string ImageUrl { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }
    }
}
