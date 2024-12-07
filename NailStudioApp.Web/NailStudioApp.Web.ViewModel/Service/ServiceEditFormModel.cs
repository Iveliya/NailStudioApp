using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Web.ViewModel.Service
{
    public class ServiceEditFormModel
    {
        [Required]
        public Guid Id { get; set; } 

        [Required]
        [StringLength(100, ErrorMessage = "Name must be between 3 and 100 characters.", MinimumLength = 3)]
        public string Name { get; set; } 

        [Required]
        [StringLength(500, ErrorMessage = "Description must be between 10 and 500 characters.", MinimumLength = 10)]
        public string Description { get; set; } 

        [Required]
        [Range(0, 10000, ErrorMessage = "Price must be between 0 and 10,000.")]
        public decimal Price { get; set; }
    }
}
