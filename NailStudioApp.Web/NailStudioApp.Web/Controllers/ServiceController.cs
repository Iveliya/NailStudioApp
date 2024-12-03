using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using NailStudioApp.Data;
//using NailStudioApp.Data.Models;
//using NailStudioApp.Web.ViewModels.NailStudio;
//using NailStudioApp.Web.ViewModels.Service;
using System.Collections.Generic;

namespace NailStudioApp.Web.Controllers
{
    //public class ServiceController : Controller
    //{
    //    private readonly  NailStudioDbContext dbContext;

    //    public ServiceController(NailStudioDbContext dbContext)
    //    {
    //        this.dbContext = dbContext;
    //    }
    //    [HttpGet]
    //    public async Task<IActionResult> Index()
    //    {
    //        var services = await dbContext.Services
    //             .Select(service => new ServiceIndexViewModel
    //             {
    //                 Id = service.Id,
    //                 Name = service.Name,
    //                 Description = service.Description,
    //                 Price = service.Price,
    //                 Duration = service.Duration,
    //                 ImageUrl = service.ImageUrl
    //             })
    //             .ToListAsync();

    //        return View(services);
    //    }
    //    [HttpGet]
    //    public IActionResult Create()
    //    {
    //        return this.View();
    //    }
    //    [HttpPost]
    //    public async Task<IActionResult> Create(AddServiceInputModel model)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            TimeSpan parsedDuration;
    //            if (!TimeSpan.TryParseExact(model.Duration, @"hh\:mm", null, out parsedDuration))
    //            {
    //                ModelState.AddModelError("Duration", "Duration must be in the format hh:mm.");
    //                return View(model);
    //            }

    //            var service = new Service
    //            {
    //                Name = model.Name,
    //                Description = model.Description,
    //                Price = model.Price,
    //                Duration = parsedDuration,
    //                ImageUrl = model.ImageUrl 
    //            };

    //            dbContext.Services.Add(service);
    //            await dbContext.SaveChangesAsync();

    //            return RedirectToAction(nameof(Index));
    //        }

    //        return View(model);
    //    }
    
    //    [HttpGet]
    //    public IActionResult Details(int id)
    //    {
    //        var service = dbContext.Services.FirstOrDefault(x=>x.Id==id);
    //        if (service != null)
    //        {
    //            return this.RedirectToAction(nameof(Index));
    //        }
           
    //        return this.View(nameof(Details));
    //    }
    //}
}
