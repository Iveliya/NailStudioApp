using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudio.Data.Models
{
    public class Service
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int DurationInMinutes { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<UserService> UserServices { get; set; } = new List<UserService>();


    }
}
