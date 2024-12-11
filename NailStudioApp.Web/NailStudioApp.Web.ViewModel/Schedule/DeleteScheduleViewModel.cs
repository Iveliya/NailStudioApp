using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Web.ViewModel.Schedule
{
    public class DeleteScheduleViewModel
    {
        [Required(ErrorMessage = "Schedule ID is required.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Staff member name is required.")]
        public string StaffMemberName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Start time is required.")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "End time is required.")]
        public DateTime EndTime { get; set; }

        public bool IsAvailable { get; set; } = true; 

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

