using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Web.ViewModel.Schedule
{
    public class DeleteScheduleViewModel
    {
       
            public Guid Id { get; set; }
            public string StaffMemberName { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public bool IsAvailable { get; set; }
        
    }

}

