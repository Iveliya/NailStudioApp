using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Web.ViewModel.Schedule
{
    public class EditScheuleViewModel
    {
        [Required(ErrorMessage = "Schedule ID is required.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Staff member is required.")]
        public Guid StaffMemberId { get; set; }

        [Required(ErrorMessage = "Start time is required.")]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "End time is required.")]
        [Display(Name = "End Time")]
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
