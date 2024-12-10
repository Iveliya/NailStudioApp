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
       
            public Guid ServiceId { get; set; } // Идентификатор на процедурата
            public DateTime AppointmentDate { get; set; } // Дата и час на запазване
            public Guid StaffMemberId { get; set; } // Идентификатор на персонала
            public List<SelectListItem> AvailableStaffMembers { get; set; } = new List<SelectListItem>();

            // Свободни часове
            public List<SelectListItem> AvailableTimes { get; set; } = new List<SelectListItem>();

            // Процедури, които може да избере потребителят
            public List<SelectListItem> AvailableServices { get; set; } = new List<SelectListItem>();
        

    }
}
