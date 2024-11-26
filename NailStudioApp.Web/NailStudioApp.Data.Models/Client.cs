using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Data.Models
{
    public class Client
    {
         public int Id { get; set; }
            public int UserId { get; set; }
            public string FirstName { get; set; } = null!;
            public string LastName { get; set; } = null!;
            public TimeSpan? PreferredTime { get; set; }

            public User User { get; set; } = null!;
            public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
            public ICollection<Review> Reviews { get; set; } = new List<Review>();

    }
}
