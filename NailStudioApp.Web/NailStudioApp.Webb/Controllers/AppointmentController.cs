using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NailStudio.Data;
using NailStudioApp.Web.ViewModel.Appointment;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using NailStudio.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace NailStudioApp.Webb.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly NailDbContext _context;
        private readonly UserManager<User> _userManager;
        public AppointmentController(NailDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
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
                //UserName = users.ContainsKey(a.UserId) ? users[a.UserId].Name : "Unknown User",
                AppointmentDate = a.AppointmentDate,
                StaffMemberName = staffMembers.ContainsKey((Guid)a.StaffMemberId) ? staffMembers[(Guid)a.StaffMemberId]?.Name : "Unassigned",
                ServicePrice = services.ContainsKey(a.ServiceId) ? services[a.ServiceId].Price.ToString("C") : "N/A"
            }).ToList();

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            //var users = await _context.Users
            //    .Select(u => new SelectListItem
            //    {
            //        Value = u.Id.ToString(),
            //        //Text = u.Name
            //    }).ToListAsync();

            //var services = await _context.Services
            //    .Select(s => new SelectListItem
            //    {
            //        Value = s.Id.ToString(),
            //        Text = s.Name
            //    }).ToListAsync();

            //var staffMembers = await _context.StaffMembers
            //    .Select(s => new SelectListItem
            //    {
            //        Value = s.Id.ToString(),
            //        Text = s.Name
            //    }).ToListAsync();

            //var viewModel = new AddAppointmentViewModel
            //{
            //    Users = users,              
            //    Services = services,         
            //    StaffMembers = staffMembers 
            //};

            //return View(viewModel);
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");  // Redirect to login page if not authenticated
            }

            var userId = _userManager.GetUserId(User);  // Get the currently logged-in user ID
            var user = await _userManager.FindByIdAsync(userId);

            // Check if the user exists, handle the error case
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Prepare the select lists for Users, Services, and StaffMembers
            var model = new AddAppointmentViewModel
            {
                UserId = user.Id,  // Pre-select the logged-in user as the UserId
                Users = _context.Users.Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Name
                }).ToList(),

                Services = _context.Services.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList(),

                StaffMembers = _context.Users.Where(u => u.Name != user.Name)  // Assuming staff are other users
                    .Select(u => new SelectListItem
                    {
                        Value = u.Id.ToString(),
                        Text = u.Name
                    }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddAppointmentViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    var appointment = new Appointment
            //    {
            //        UserId = viewModel.UserId,
            //        ServiceId = viewModel.ServiceId,
            //        AppointmentDate = viewModel.AppointmentDate,
            //        StaffMemberId = viewModel.StaffMemberId
            //    };

            //    _context.Appointments.Add(appointment);
            //    await _context.SaveChangesAsync();

            //    return RedirectToAction("Index");
            //}

            //var users = await _context.Users
            //    .Select(u => new SelectListItem
            //    {
            //        Value = u.Id.ToString(),
            //        //Text = u.Name
            //    }).ToListAsync();

            //var services = await _context.Services
            //    .Select(s => new SelectListItem
            //    {
            //        Value = s.Id.ToString(),
            //        Text = s.Name
            //    }).ToListAsync();

            //var staffMembers = await _context.StaffMembers
            //    .Select(s => new SelectListItem
            //    {
            //        Value = s.Id.ToString(),
            //        Text = s.Name
            //    }).ToListAsync();

            //viewModel.Users = users;
            //viewModel.Services = services;
            //viewModel.StaffMembers = staffMembers;

            //return View(viewModel);
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");  // Redirect to login page if not authenticated
            }

            if (ModelState.IsValid)
            {
                // Create the appointment
                var appointment = new Appointment
                {
                    UserId = model.UserId,
                    ServiceId = model.ServiceId,
                    AppointmentDate = model.AppointmentDate,
                    //StaffMemberId = model.StaffMemberId
                };

                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");  // Redirect to some appointment list or confirmation page
            }

            // If the model is not valid, reload the dropdowns and return the view again
            model.Users = _context.Users.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Name
            }).ToList();

            model.Services = _context.Services.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();

            model.StaffMembers = _context.Users.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Name
            }).ToList();

            return View(model);
        }
    }
}
