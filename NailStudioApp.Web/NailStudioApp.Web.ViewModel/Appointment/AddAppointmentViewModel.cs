using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace NailStudioApp.Web.ViewModel.Appointment
{
    public class AddAppointmentViewModel
    {
        public Guid UserId { get; set; } 
        public Guid ServiceId { get; set; } 
        public DateTime AppointmentDate { get; set; } 
        public Guid? StaffMemberId { get; set; } 

        public IEnumerable<SelectListItem> Users { get; set; }
        public IEnumerable<SelectListItem> Services { get; set; }
        public IEnumerable<SelectListItem> StaffMembers { get; set; }
    }
}
