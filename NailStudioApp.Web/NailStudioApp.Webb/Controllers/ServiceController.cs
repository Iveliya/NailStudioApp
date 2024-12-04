using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NailStudio.Data;
using NailStudio.Data.Models;
using NailStudioApp.Web.ViewModel.Service;

namespace NailStudioApp.Webb.Controllers
{
    public class ServiceController : Controller
    {
        private readonly NailDbContext _context;

        public ServiceController(NailDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var services = await _context.Services
             .Select(s => new ServiceIndexViewModel
             {
                 Id = s.Id,
                 Name = s.Name,
                 Price = s.Price,
                 DurationInMinutes = s.DurationInMinutes,
                 ImageUrl = s.ImageUrl,
             })
             .ToListAsync();

            return View(services);
        }

        [HttpGet]
        public IActionResult AddService()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddService(AddServiceFormModel model)
        {
            if (ModelState.IsValid)
            {
                var service = new Service
                {
                    Name = model.Name,
                    Price = model.Price,
                    DurationInMinutes = model.DurationInMinutes,
                    ImageUrl = model.ImageUrl,
                    Description = model.Description
                };

                await _context.Services.AddAsync(service);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            var service = await _context.Services.FindAsync(id); 

            if (service == null)
            {
                return NotFound(); 
            }

            var viewModel = new DetailServiceViewModel
            {
                Id = service.Id,
                Name = service.Name,
                Price = service.Price,
                DurationInMinutes = service.DurationInMinutes,
                ImageUrl = service.ImageUrl,
                Description = service.Description
            };

            return View(viewModel);
        }
    }
}
