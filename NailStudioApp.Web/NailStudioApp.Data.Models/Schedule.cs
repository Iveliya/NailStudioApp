using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Data.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int DayOfWeek { get; set; } // 0 = Sunday, 1 = Monday, etc.
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public Employee Employee { get; set; } = null!;

    }
}
