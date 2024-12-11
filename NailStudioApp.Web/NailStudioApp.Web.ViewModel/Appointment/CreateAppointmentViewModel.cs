using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NailStudioApp.Web.ViewModel.Appointment
{
    public class CreateAppointmentViewModel
    {
        [Required(ErrorMessage = "Please select a service.")]
        public Guid ServiceId { get; set; }

        [Required(ErrorMessage = "Please select a staff member.")]
        public Guid StaffMemberId { get; set; }

        [Required(ErrorMessage = "Please select a date for your appointment.")]
        public DateTime SelectedDate { get; set; }

        [Required(ErrorMessage = "Please select a time for your appointment.")]
        public TimeSpan SelectedTime { get; set; }

        [Required(ErrorMessage = "The services list is required.")]
        public List<SelectListItem> Services { get; set; } = new();

        [Required(ErrorMessage = "The staff members list is required.")]
        public List<SelectListItem> StaffMembers { get; set; } = new();

        [Required(ErrorMessage = "Available time slots are required.")]
        public List<SelectListItem> AvailableTimes { get; set; } = new();
    }
}
