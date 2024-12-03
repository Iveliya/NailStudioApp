using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Web.ViewModels.Appointment
{
    using static Common.EntityValidationConstans.Appointment;
    public class AddAppointmentFormModel
    {

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [Range(0.01, 10000, ErrorMessage = "Total Price must be between 0.01 and 10,000.")]
        public decimal TotalPrice { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        public string Status { get; set; } = StatusDefaultValue;

    }
}
