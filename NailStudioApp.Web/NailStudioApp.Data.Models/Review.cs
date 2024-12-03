using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Data.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Comment { get; set; } = null!;
        public int Rating { get; set; } // From 1 to 5
        public DateTime ReviewDate { get; set; }


    }
}
