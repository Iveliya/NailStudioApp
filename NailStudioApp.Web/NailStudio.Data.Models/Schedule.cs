using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudio.Data.Models
{
    public class Schedule
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid StaffMemberId { get; set; }
        public StaffMember StaffMember { get; set; } = null!;
        public bool IsAvailable { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
