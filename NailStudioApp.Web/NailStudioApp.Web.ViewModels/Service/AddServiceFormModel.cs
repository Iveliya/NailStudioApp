using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Web.ViewModels.Service
{
    public class AddServiceFormModel
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string Duration { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
    }
}
