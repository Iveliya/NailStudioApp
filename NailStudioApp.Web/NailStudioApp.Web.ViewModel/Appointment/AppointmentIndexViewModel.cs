using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Web.ViewModel.Appointment
{
    public class AppointmentIndexViewModel
    {
        
        [Required(ErrorMessage = "Appointment ID is required.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Service Name is required.")]
        [StringLength(100, ErrorMessage = "Service Name cannot exceed 100 characters.")]
        public string ServiceName { get; set; } = string.Empty;

        [Required(ErrorMessage = "User Name is required.")]
        [StringLength(50, ErrorMessage = "User Name cannot exceed 50 characters.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Appointment Date is required.")]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "Staff Member Name is required.")]
        [StringLength(50, ErrorMessage = "Staff Member Name cannot exceed 50 characters.")]
        public string StaffMemberName { get; set; } = string.Empty;

        
        [Required(ErrorMessage = "Service Price is required.")]
        [RegularExpression(@"^\$?\d+(\.\d{2})?$", ErrorMessage = "Service Price must be a valid currency value.")]
        public string ServicePrice { get; set; } = string.Empty;
        public string FormattedDate => AppointmentDate.ToString("dddd, MMM dd, yyyy hh:mm tt"); 

    }
}
