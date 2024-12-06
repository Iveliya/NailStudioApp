using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudio.Data.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = null!;
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;
    }
}
