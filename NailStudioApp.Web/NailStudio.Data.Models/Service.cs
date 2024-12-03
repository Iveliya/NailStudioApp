using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudio.Data.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public TimeSpan Duration { get; set; }
        public string ImageUrl { get; set; } = null!;

    }
}
