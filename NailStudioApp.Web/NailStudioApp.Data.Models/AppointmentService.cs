using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Data.Models
{
    public class AppointmentService
    {
        public int AppointmentId { get; set; }
        public int ServiceId { get; set; }

        public Appointment Appointment { get; set; } = null!;
        public Service Service { get; set; } = null!;

    }
}
