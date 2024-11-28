using Microsoft.AspNetCore.Mvc;
using NailStudioApp.Data;
using NailStudioApp.Data.Models;
using Org.BouncyCastle.Utilities.Collections;
using System.Collections.Generic;

namespace NailStudioApp.Web.Controllers
{
    public class NailStudioController : Controller
    {
        private readonly  NailStudioDbContext dbContext;

        public NailStudioController(NailStudioDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Service> allServices = dbContext.Services.ToList();
            return View(allServices);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }
        [HttpPost]
        public IActionResult Create(Service service)
        {
            this.dbContext.Services.Add(service);
            this.dbContext.SaveChanges();
            return this.RedirectToAction(nameof(Index));  
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var service = dbContext.Services.FirstOrDefault(x=>x.Id==id);
            if (service != null)
            {
                return this.RedirectToAction(nameof(Index));
            }
           
            return this.View(nameof(Details));
        }
    }
}
