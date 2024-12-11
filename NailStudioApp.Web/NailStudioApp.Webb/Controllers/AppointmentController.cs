using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NailStudio.Data;
using NailStudioApp.Web.ViewModel.Appointment;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using NailStudio.Data.Models;
using Microsoft.AspNetCore.Identity;
using NailStudioApp.Web.Infrastruction.Extensions;
using System.Security.Claims;
using AutoMapper.QueryableExtensions;
using NailStudioApp.Web.ViewModel.Service;
using AutoMapper;

namespace NailStudioApp.Webb.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly NailDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public AppointmentController(NailDbContext context, UserManager<User> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var currentUserId = _userManager.GetUserId(User);

            if (currentUserId == null)
            {
                return RedirectToAction("Login", "Account"); 
            }

            var appointments = await _context.Appointments
                .Where(a => a.UserId.ToString() == currentUserId)
                .ToListAsync();

            var services = await _context.Services.ToDictionaryAsync(s => s.Id);
            var users = await _context.Users.ToDictionaryAsync(u => u.Id);
            var staffMembers = await _context.StaffMembers.ToDictionaryAsync(s => s.Id);

            var viewModel = appointments.Select(a => new AppointmentIndexViewModel
            {
                Id = a.Id,
                ServiceName = services.ContainsKey(a.ServiceId) ? services[a.ServiceId].Name : "Unknown Service",
                AppointmentDate = a.AppointmentDate,
                StaffMemberName = staffMembers.ContainsKey((Guid)a.StaffMemberId) ? staffMembers[(Guid)a.StaffMemberId]?.Name : "Unassigned",
                ServicePrice = services.ContainsKey(a.ServiceId) ? services[a.ServiceId].Price.ToString("C") : "N/A"
            }).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            var appointments = await _context.Appointments
                .ProjectTo<AppointmentIndexViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return View(appointments);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var appointment = await _context.Appointments
                .Where(a => a.Id == id && !a.IsDeleted)
                .FirstOrDefaultAsync();

            if (appointment == null)
            {
                return NotFound();
            }

            var services = await _context.Services
                .Where(s => !s.IsDeleted)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                })
                .ToListAsync();

            var staffMembers = await _context.StaffMembers
                .Where(sm => !sm.IsDeleted)
                .Select(sm => new SelectListItem
                {
                    Value = sm.Id.ToString(),
                    Text = sm.Name
                })
                .ToListAsync();

            var model = new AppointmentViewModel
            {
                ServiceId = appointment.ServiceId,
                StaffMemberId = appointment.StaffMemberId,
                SelectedDate = appointment.AppointmentDate.Date,
                SelectedTime = appointment.AppointmentDate.ToString("hh:mm tt"),
                Services = services,
                StaffMembers = staffMembers
            };

            return View(model);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid serviceId, Guid staffMemberId, DateTime selectedDate, string selectedTime)
        {
            TimeSpan time = TimeSpan.Parse(selectedTime);

            var appointment = await _context.Appointments
                .Where(a => a.ServiceId == serviceId &&
                            a.StaffMemberId == staffMemberId &&
                            a.AppointmentDate == selectedDate.Date.Add(time) &&
                            !a.IsDeleted)
                .FirstOrDefaultAsync();

            if (appointment == null)
            {
                return NotFound();
            }

            appointment.IsDeleted = true;

            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Manage));
        }


        

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var services = await _context.Services
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                })
                .ToListAsync();

            // Get available staff members
            var staffMembers = await _context.StaffMembers
                .Select(sm => new SelectListItem
                {
                    Value = sm.Id.ToString(),
                    Text = sm.Name
                })
                .ToListAsync();

            var model = new AppointmentViewModel
            {
                Services = services,
                StaffMembers = staffMembers
            };

            return View(model);
        }

        // GET: Fetch available times based on selected date and staff member
        [HttpGet]
        public JsonResult GetAvailableTimes(Guid serviceId, Guid staffMemberId, DateTime selectedDate)
        {
            var staffMember = _context.StaffMembers.FirstOrDefault(sm => sm.Id == staffMemberId);
            if (staffMember == null)
            {
                return Json(new { success = false });
            }

            var availableTimes = GetAvailableTimes(staffMember, selectedDate);

            var times = availableTimes.Select(at => new
            {
                value = at.ToString(),
                text = at.ToString(@"hh\:mm")
            }).ToList();

            return Json(times);
        }

        private List<TimeSpan> GetAvailableTimes(StaffMember staffMember, DateTime selectedDate)
        {
            var workStartTime = new TimeSpan(9, 0, 0);
            var workEndTime = new TimeSpan(17, 0, 0);
            var availableTimes = new List<TimeSpan>();

            var selectedDateNormalized = selectedDate.Date;
            for (var time = workStartTime; time < workEndTime; time = time.Add(TimeSpan.FromMinutes(30)))
            {
                var timeSlot = _context.TimeSlots
           .FirstOrDefault(ts => ts.StaffMemberId == staffMember.Id
                                && ts.Date.Date == selectedDateNormalized  
                                && ts.Time == time  
                                && ts.IsBooked);   


                if (timeSlot == null)
                {
                    availableTimes.Add(time);
                }
            }

            return availableTimes;
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppointmentViewModel model)
        {
            var services = await _context.Services
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                })
                .ToListAsync();

            var staffMembers = await _context.StaffMembers
                .Select(sm => new SelectListItem
                {
                    Value = sm.Id.ToString(),
                    Text = sm.Name
                })
                .ToListAsync();

            //var datas = GetAvailableTimesForBooking(model.ServiceId, model.StaffMemberId, model.SelectedDate);
            model.Services = services;
            model.StaffMembers = staffMembers;
            var availableTimes = await GetAvailableTimesForBooking(model.ServiceId, model.StaffMemberId, model.SelectedDate);

            model.AvailableTimes = availableTimes.Select(at => new SelectListItem
            {
                Value = at.ToString(),
                Text = at.ToString(@"hh\:mm")
            }).ToList();
            var staffMember = await _context.StaffMembers.FindAsync(model.StaffMemberId);
            var service = await _context.Services.FindAsync(model.ServiceId);

            if (staffMember == null || service == null)
            {
                ModelState.AddModelError("", "Selected service or staff member is invalid.");
                return View(model);
            }

            var selectedTime = model.AvailableTimes?.FirstOrDefault()?.Value;
            if (string.IsNullOrEmpty(selectedTime))
            {
                ModelState.AddModelError("", "Please select a valid time.");
                return View(model);
            }

            var appointmentDate = model.SelectedDate.Date.Add(TimeSpan.Parse(selectedTime));
            var appointmentTime = TimeSpan.Parse(selectedTime);
            int serviceDuration = service.DurationInMinutes;
            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(_userManager.GetUserId(User)),
                ServiceId = service.Id,
                StaffMemberId = staffMember.Id,
                AppointmentDate = appointmentDate
            };
            List<TimeSlot> timeSlots = new List<TimeSlot>();

            for (int i = 0; i < serviceDuration / 30; i++) 
            {
                var timeSlot = new TimeSlot
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.Parse(_userManager.GetUserId(User)),
                    Date = appointmentDate.Date,
                    Time = appointmentTime.Add(TimeSpan.FromMinutes(i * 30)),  
                    IsBooked = true,
                    ServiceId = service.Id,
                    StaffMemberId = staffMember.Id
                };

                timeSlots.Add(timeSlot);
            }

            foreach (var x in timeSlots)
            {
                _context.TimeSlots.Add(x);
                await _context.SaveChangesAsync();
            }

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Appointment");
            
        }


        private async Task PopulateDropdowns(AppointmentViewModel model)
        {
            model.Services = await _context.Services
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                })
                .ToListAsync();

            model.StaffMembers = await _context.StaffMembers
                .Select(sm => new SelectListItem
                {
                    Value = sm.Id.ToString(),
                    Text = sm.Name
                })
                .ToListAsync();

            var availableTimes = await GetAvailableTimesForBooking(model.ServiceId, model.StaffMemberId, model.SelectedDate);
            model.AvailableTimes = availableTimes.Select(at => new SelectListItem
            {
                Value = at.ToString(),
                Text = at.ToString(@"hh\:mm")
            }).ToList();
        }


        private async Task<List<TimeSpan>> GetAvailableTimesForBooking(Guid serviceId, Guid staffMemberId, DateTime selectedDate)
        {
            var staffMember = await _context.StaffMembers.FirstOrDefaultAsync(sm => sm.Id == staffMemberId);
            if (staffMember == null)
            {
                return null;
            }

            var availableTimes = new List<TimeSpan>();
            var workStartTime = new TimeSpan(9, 0, 0);
            var workEndTime = new TimeSpan(17, 0, 0);

            for (var time = workStartTime; time < workEndTime; time = time.Add(TimeSpan.FromMinutes(30)))
            {
                var timeSlot = _context.TimeSlots
                    .FirstOrDefault(ts => ts.StaffMemberId == staffMember.Id
                                        && ts.Date.Date == selectedDate.Date
                                        && ts.Time == time
                                        && ts.IsBooked);

                if (timeSlot == null)
                {
                    availableTimes.Add(time);
                }
            }

            return availableTimes;
        }


    }
}
