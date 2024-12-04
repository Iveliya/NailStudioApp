using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudio.Data.Models
{
    public class StaffMember
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string Specialization { get; set; } = null!;
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    }
}
