using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Web.ViewModel.Schedule
{
    public class AddScheduleViewModel
    {
        public Guid StaffMemberId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsAvailable { get; set; } = true;

        public List<SelectListItem> StaffMembers { get; set; } = new List<SelectListItem>(); 

        public string ValidateTimes()
        {
            if (EndTime <= StartTime)
            {
                return "End time must be after start time.";
            }
            return null;
        }
    }
}
