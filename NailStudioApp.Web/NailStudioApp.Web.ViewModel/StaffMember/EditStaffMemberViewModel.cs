using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Web.ViewModel.StaffMember
{
    public class EditStaffMemberViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Staff member name is required.")]
        [StringLength(100, ErrorMessage = "Staff member name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [StringLength(50, ErrorMessage = "Role cannot exceed 50 characters.")]
        public string Role { get; set; }

        [Url(ErrorMessage = "Please provide a valid URL for the photo.")]
        [StringLength(200, ErrorMessage = "Photo URL cannot exceed 200 characters.")]
        public string PhotoUrl { get; set; }
    }
}
