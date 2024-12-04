using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Web.ViewModel.Appointment
{
    public class AppointmentIndexViewModel
    {
        public Guid Id { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public DateTime AppointmentDate { get; set; }
        public string StaffMemberName { get; set; } = string.Empty;
        public string ServicePrice { get; set; } = string.Empty;
        public string FormattedDate => AppointmentDate.ToString("dddd, MMM dd, yyyy hh:mm tt"); 

    }
}
