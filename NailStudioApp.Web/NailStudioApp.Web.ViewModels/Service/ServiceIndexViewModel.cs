﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Web.ViewModels.Service
{
    public class ServiceIndexViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public TimeSpan Duration { get; set; }
        public string ImageUrl { get; set; }
    }
}
