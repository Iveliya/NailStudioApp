using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NailStudio.Data;
using NailStudio.Data.Models;
using NailStudio.Data.Repository.Interfaces;
using NailStudioApp.Services.Data;
using NailStudioApp.Services.Data.Interfaces;
using NailStudioApp.Web.ViewModel.Service;
using NuGet.Protocol.Core.Types;

namespace NailStudioApp.Webb.Controllers
{
    using NailStudio.Data.Models;
    using NailStudioApp.Web.ViewModel.Service;
    public class ServiceController : Controller
    {
        private readonly NailDbContext _context;
        private readonly IMapper _mapper;
        public ServiceController(NailDbContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

       
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var serviceViewModels = await _context.Services
        .ProjectTo<ServiceIndexViewModel>(_mapper.ConfigurationProvider)
        .ToListAsync();

            return View(serviceViewModels);

        }
        [HttpGet]
        public IActionResult AddManage()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddManage(AddServiceFormModel model)
        {
            if (ModelState.IsValid)
            {
                var service = _mapper.Map<Service>(model);

                await _context.Services.AddAsync(service);
                await _context.SaveChangesAsync();

                return RedirectToAction("Manage");
            }

            return View(model);
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
                var service = _mapper.Map<Service>(model);

                await _context.Services.AddAsync(service);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            


            var service = await _context.Services
        .Where(s => s.Id == id) 
        .FirstOrDefaultAsync();  

            if (service == null)
            {
                return NotFound(); 
            }

            var serviceViewModel = _mapper.Map<DetailServiceViewModel>(service);

            return View(serviceViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DetailManage(Guid id)
        {
            var service = await _context.Services
        .Where(s => s.Id == id)
        .FirstOrDefaultAsync();

            if (service == null)
            {
                return NotFound();
            }

            var serviceViewModel = _mapper.Map<DetailServiceViewModel>(service);

            return View(serviceViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            var services = await _context.Services
                .ProjectTo<ServiceIndexViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return View(services);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            
            var service = await _context.Services
           .Where(s => s.Id == id && !s.IsDeleted) 
           .FirstOrDefaultAsync();

            if (service == null)
            {
                return NotFound();
            }

            var serviceViewModel = _mapper.Map<ServiceEditFormModel>(service);
            return View(serviceViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ServiceEditFormModel model)
        {
            
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var service = await _context.Services
                    .Where(s => s.Id == id && !s.IsDeleted) 
                    .FirstOrDefaultAsync();

                if (service == null)
                {
                    return NotFound();
                }

                _mapper.Map(model, service);
                _context.Services.Update(service);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Manage));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var service = await _context.Services
                .Where(s => s.Id == id && !s.IsDeleted) 
                .FirstOrDefaultAsync();

            if (service == null)
            {
                return NotFound();
            }

            var serviceViewModel = _mapper.Map<ServiceEditFormModel>(service);
            return View(serviceViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var service = await _context.Services
                .Where(s => s.Id == id && !s.IsDeleted)
                .FirstOrDefaultAsync();

            if (service == null)
            {
                return NotFound();
            }

            service.IsDeleted = true;

            _context.Services.Update(service); 
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Manage));
        }
    }
}
