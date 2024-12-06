using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudio.Data.Models
{
    public class User:IdentityUser<Guid>
    {
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<UserService> UserServices { get; set; } = new List<UserService>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
