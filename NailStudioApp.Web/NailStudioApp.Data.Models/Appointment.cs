using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Data.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Pending";

        public Employee Employee { get; set; } = null!;
        public ICollection<AppointmentService> AppointmentServices { get; set; } = new List<AppointmentService>();

    }
}
