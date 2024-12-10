using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudio.Data.Models
{
    public class TimeSlot
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

        public bool IsBooked { get; set; } = false;

        public Guid? UserId { get; set; }
        public User? User { get; set; }

        public Guid StaffMemberId { get; set; }
        public StaffMember StaffMember { get; set; } = null!;

        public Guid? ServiceId { get; set; }
        public Service? Service { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
