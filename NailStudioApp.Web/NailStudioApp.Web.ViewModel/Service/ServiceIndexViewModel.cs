using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Web.ViewModel.Service
{
    public class ServiceIndexViewModel
    {
        public Guid Id { get; set; } 
        public string Name { get; set; } 
        public decimal Price { get; set; } 
        public int DurationInMinutes { get; set; } 
        public string ImageUrl { get; set; }
    }
}
