using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NailStudio.Data;
using NailStudioApp.Web.ViewModel.Appointment;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using NailStudio.Data.Models;

namespace NailStudioApp.Webb.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly NailDbContext _context;

        public AppointmentController(NailDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var appointments = await _context.Appointments.ToListAsync();

            var services = await _context.Services.ToDictionaryAsync(s => s.Id);
            var users = await _context.Users.ToDictionaryAsync(u => u.Id);
            var staffMembers = await _context.StaffMembers.ToDictionaryAsync(s => s.Id);

            var viewModel = appointments.Select(a => new AppointmentIndexViewModel
            {
                Id = a.Id,
                ServiceName = services.ContainsKey(a.ServiceId) ? services[a.ServiceId].Name : "Unknown Service",
                UserName = users.ContainsKey(a.UserId) ? users[a.UserId].Name : "Unknown User",
                AppointmentDate = a.AppointmentDate,
                StaffMemberName = staffMembers.ContainsKey((Guid)a.StaffMemberId) ? staffMembers[(Guid)a.StaffMemberId]?.Name : "Unassigned",
                ServicePrice = services.ContainsKey(a.ServiceId) ? services[a.ServiceId].Price.ToString("C") : "N/A"
            }).ToList();

            return View(viewModel);
        }
        public async Task<IActionResult> Create()
        {
            var users = await _context.Users
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Name
                }).ToListAsync();

            var services = await _context.Services
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToListAsync();

            var staffMembers = await _context.StaffMembers
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToListAsync();

            var viewModel = new AddAppointmentViewModel
            {
                Users = users,              
                Services = services,         
                StaffMembers = staffMembers 
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddAppointmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var appointment = new Appointment
                {
                    UserId = viewModel.UserId,
                    ServiceId = viewModel.ServiceId,
                    AppointmentDate = viewModel.AppointmentDate,
                    StaffMemberId = viewModel.StaffMemberId
                };

                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            var users = await _context.Users
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Name
                }).ToListAsync();

            var services = await _context.Services
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToListAsync();

            var staffMembers = await _context.StaffMembers
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToListAsync();

            viewModel.Users = users;
            viewModel.Services = services;
            viewModel.StaffMembers = staffMembers;

            return View(viewModel);
        }
    }
}
