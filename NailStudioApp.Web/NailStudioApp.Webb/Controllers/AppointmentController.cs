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
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NailStudio.Data;

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


        //[HttpGet]
        //public IActionResult Create()
        //{
        //    var viewModel = new AddAppointmentViewModel
        //    {
        //        // Свободни служители
        //        AvailableStaffMembers = _context.StaffMembers
        //         .Where(s => !s.IsDeleted)
        //         .Select(s => new SelectListItem
        //         {
        //             Value = s.Id.ToString(),
        //             Text = s.Name
        //         }).ToList(),

        //        // Свободни часове
        //        AvailableTimes = GetAvailableTimes(DateTime.Now),

        //        // Процедури
        //        AvailableServices = _context.Services
        //         .Where(s => !s.IsDeleted)
        //         .Select(s => new SelectListItem
        //         {
        //             Value = s.Id.ToString(),
        //             Text = s.Name
        //         }).ToList()
        //    };

        //    return View(viewModel);

        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(AddAppointmentViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Проверка дали избраният час е в бъдещето
        //        if (model.AppointmentDate <= DateTime.Now)
        //        {
        //            ModelState.AddModelError("", "Не можете да запазите час в миналото.");
        //            return View(model);
        //        }

        //        // Проверка дали часът е свободен за избрания персонал
        //        var isAvailable = !await _context.Appointments
        //            .AnyAsync(a => a.StaffMemberId == model.StaffMemberId &&
        //                           a.AppointmentDate == model.AppointmentDate &&
        //                           !a.IsDeleted);

        //        if (!isAvailable)
        //        {
        //            ModelState.AddModelError("", "Избраният час е зает. Моля, изберете друг.");
        //            return View(model);
        //        }

        //        // Създаване на новия запис за час
        //        var appointment = new Appointment
        //        {
        //            UserId = (await _userManager.GetUserAsync(User))?.Id ?? Guid.Empty, // Получаваме текущия потребител
        //            ServiceId = model.ServiceId,
        //            StaffMemberId = model.StaffMemberId,
        //            AppointmentDate = model.AppointmentDate,
        //        };

        //        _context.Appointments.Add(appointment);
        //        await _context.SaveChangesAsync();

        //        return RedirectToAction("Index"); // Пренасочване към страница със записите или успешен резултат
        //    }

        //    // Ако има грешки при валидацията, връщаме формата с нови данни
        //    model.AvailableStaffMembers = _context.StaffMembers
        //        .Where(s => !s.IsDeleted)
        //        .Select(s => new SelectListItem
        //        {
        //            Value = s.Id.ToString(),
        //            Text = s.Name
        //        }).ToList();

        //    model.AvailableServices = _context.Services
        //        .Where(s => !s.IsDeleted)
        //        .Select(s => new SelectListItem
        //        {
        //            Value = s.Id.ToString(),
        //            Text = s.Name
        //        }).ToList();

        //    model.AvailableTimes = GetAvailableTimes(model.AppointmentDate);

        //    return View(model);
        //}

        //// Помощен метод за извличане на свободни часове
        //private List<SelectListItem> GetAvailableTimes(DateTime date)
        //{
        //    var availableTimes = new List<SelectListItem>();
        //    var startOfDay = date.Date.AddHours(9); // Започваме от 9 сутринта
        //    var endOfDay = date.Date.AddHours(18); // Завършваме в 6 вечерта

        //    // Добавяме слотове на всеки 30 минути
        //    for (var time = startOfDay; time <= endOfDay; time = time.AddMinutes(30))
        //    {
        //        availableTimes.Add(new SelectListItem
        //        {
        //            Value = time.ToString("yyyy-MM-dd HH:mm"),
        //            Text = time.ToString("hh:mm tt")
        //        });
        //    }

        //    return availableTimes;
        //}


        //[HttpGet]
        //public async Task<IActionResult> Create()
        //{
        //    var viewModel = new AddAppointmentViewModel
        //    {
        //        // Попълваме възможни услуги и служители от базата данни асинхронно
        //        AvailableServices = await _context.Services.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
        //        {
        //            Text = s.Name,
        //            Value = s.Id.ToString()
        //        }).ToListAsync(),

        //        AvailableStaffMembers = await _context.StaffMembers.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
        //        {
        //            Text = s.Name,
        //            Value = s.Id.ToString()
        //        }).ToListAsync()
        //    };

        //    return View(viewModel);
        //}

        //// POST метод за създаване на нова среща
        //[HttpPost]
        //public async Task<IActionResult> Create(AddAppointmentViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Вземане на текущия потребител асинхронно
        //        var user = await _userManager.GetUserAsync(User);

        //        // Проверка дали текущия потребител съществува
        //        if (user == null)
        //        {
        //            return RedirectToAction("Login", "Account"); // Или друга логика за обработка на неавторизирани потребители
        //        }

        //        // Проверка дали избраната дата и час са свободни за избрания служител асинхронно
        //        var existingAppointment = await _context.Appointments
        //            .FirstOrDefaultAsync(a => a.StaffMemberId == model.StaffMemberId
        //                                 && a.AppointmentDate == model.AppointmentDate
        //                                 && !a.IsDeleted); // Проверка дали срещата не е изтрита

        //        if (existingAppointment != null)
        //        {
        //            ModelState.AddModelError("", "Този час вече е зает.");
        //            //TempData["ErrorMessage"] = "Този час вече е зает.";
        //            model.AvailableServices = await _context.Services.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
        //            {
        //                Text = s.Name,
        //                Value = s.Id.ToString()
        //            }).ToListAsync();

        //            model.AvailableStaffMembers = await _context.StaffMembers.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
        //            {
        //                Text = s.Name,
        //                Value = s.Id.ToString()
        //            }).ToListAsync();

        //            return View(model);
        //        }

        //        // Създаване на нова среща
        //        var appointment = new Appointment
        //        {
        //            Id = Guid.NewGuid(),
        //            UserId = user.Id, // Вземаме ID-то на текущия потребител
        //            ServiceId = model.ServiceId,
        //            AppointmentDate = model.AppointmentDate,
        //            StaffMemberId = model.StaffMemberId,
        //            IsDeleted = false
        //        };

        //        // Записваме новата среща в базата данни асинхронно
        //        _context.Appointments.Add(appointment);
        //        await _context.SaveChangesAsync();

        //        // Пренасочваме към друга страница след успешно създаване
        //        return RedirectToAction("Index", "Home"); // Променете към вашата желана страница
        //    }

        //    // Ако моделът не е валиден, показваме отново същата форма с грешки
        //    return View(model);
        //}

        ////// Метод за получаване на налични часове
        ////public async Task<JsonResult> GetAvailableTimes(Guid staffMemberId, DateTime date)
        ////{
        ////    if (date.DayOfWeek == DayOfWeek.Sunday)
        ////    {
        ////        return Json(new { error = "Не се работи в неделя." });
        ////    }

        ////    var availableTimes = new System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();

        ////    // Работно време от 9:00 до 17:00, всяка минута по 30 минути
        ////    for (var hour = 9; hour < 17; hour++)
        ////    {
        ////        availableTimes.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
        ////        {
        ////            Text = $"{hour}:00",
        ////            Value = $"{hour}:00"
        ////        });
        ////        availableTimes.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
        ////        {
        ////            Text = $"{hour}:30",
        ////            Value = $"{hour}:30"
        ////        });
        ////    }

        ////    // Връщаме наличните часове
        ////    return Json(new { availableTimes });
        ////}
        //[HttpGet]
        //public async Task<IActionResult> GetAvailableTimes(Guid staffMemberId, DateTime date)
        //{
        //    // Проверка дали е в неделя (или друг ден, когато не се работи)
        //    if (date.DayOfWeek == DayOfWeek.Sunday)
        //    {
        //        return Json(new { error = "Не се работи в неделя." });
        //    }

        //    // Работно време от 9:00 до 17:00
        //    var allTimes = new List<TimeSpan>();
        //    for (var hour = 9; hour < 17; hour++)
        //    {
        //        allTimes.Add(new TimeSpan(hour, 0, 0)); // 9:00, 10:00 и т.н.
        //        allTimes.Add(new TimeSpan(hour, 30, 0)); // 9:30, 10:30 и т.н.
        //    }

        //    // Взимаме запазените часове за избрания служител и дата
        //    var bookedTimes = await _context.Appointments
        //        .Where(a => a.StaffMemberId == staffMemberId
        //                    && a.AppointmentDate.Date == date.Date
        //                    && !a.IsDeleted) // Изключваме изтритите срещи
        //        .Select(a => a.AppointmentDate.TimeOfDay) // Вземаме само часове
        //        .ToListAsync();

        //    // Филтрираме всички часове и връщаме само свободните
        //    var availableTimes = allTimes
        //        .Where(t => !bookedTimes.Contains(t)) // Изключваме заетите
        //        .Select(t => new
        //        {
        //            Text = t.ToString(@"hh\:mm"), // Формат на часа
        //            Value = t.ToString(@"hh\:mm") // Стойността, която ще изпратим
        //        })
        //        .ToList();

        //    return Json(new { availableTimes });
        //}

        // GET: Create Appointment
        // Step 1: Show form for selecting service, staff member and date
        //public IActionResult Create()
        //{
        //    var viewModel = new AppointmentViewModel
        //    {
        //        Services = _context.Services
        //            .Where(s => !s.IsDeleted)
        //            .Select(s => new SelectListItem
        //            {
        //                Value = s.Id.ToString(),
        //                Text = s.Name
        //            }).ToList(),
        //        StaffMembers = _context.StaffMembers
        //            .Where(sm => !sm.IsDeleted)
        //            .Select(sm => new SelectListItem
        //            {
        //                Value = sm.Id.ToString(),
        //                Text = sm.Name
        //            }).ToList(),
        //        SelectedDate = DateTime.Today // Default to today's date
        //    };

        //    return View(viewModel);
        //}
        //[HttpGet]
        //public async Task<IActionResult> Create()
        //{
        //    // Fetch available services from the database
        //    var services = await _context.Services
        //        .Select(s => new SelectListItem
        //        {
        //            Value = s.Id.ToString(),
        //            Text = s.Name // Assuming `Name` is a property of Service
        //        })
        //        .ToListAsync();

        //    // Fetch available staff members from the database
        //    var staffMembers = await _context.StaffMembers
        //        .Select(sm => new SelectListItem
        //        {
        //            Value = sm.Id.ToString(),
        //            Text = sm.Name // Assuming `Name` is a property of StaffMember
        //        })
        //        .ToListAsync();

        //    // Prepare the view model
        //    var createAppointmentViewModel = new AppointmentViewModel
        //    {
        //        Services = services, // Populate Services
        //        StaffMembers = staffMembers, // Populate StaffMembers
        //    };

        //    return View(createAppointmentViewModel);
        //}


        //// Step 2: When user selects service, staff member and date, show available times
        //[HttpPost]
        //public IActionResult Create(AppointmentViewModel model)
        //{
        //    var services =  _context.Services
        //       .Select(s => new SelectListItem
        //       {
        //           Value = s.Id.ToString(),
        //           Text = s.Name // Assuming `Name` is a property of Service
        //       })
        //       .ToList();

        //    var staffMembers =  _context.StaffMembers
        //       .Select(sm => new SelectListItem
        //       {
        //           Value = sm.Id.ToString(),
        //           Text = sm.Name // Assuming `Name` is a property of StaffMember
        //       })
        //       .ToList();
        //    // Check if the selected date is valid
        //    if (model.SelectedDate < DateTime.Today)
        //    {
        //        ModelState.AddModelError("SelectedDate", "The selected date must be after today.");
        //        model.Services = _context.Services
        //            .Where(s => !s.IsDeleted)
        //            .Select(s => new SelectListItem
        //            {
        //                Value = s.Id.ToString(),
        //                Text = s.Name
        //            }).ToList();
        //        model.StaffMembers = _context.StaffMembers
        //            .Where(sm => !sm.IsDeleted)
        //            .Select(sm => new SelectListItem
        //            {
        //                Value = sm.Id.ToString(),
        //                Text = sm.Name
        //            }).ToList();
        //        return View(model);
        //    }

        //    // Fetch available times based on selected staff member and date
        //    var staffMember = _context.StaffMembers.FirstOrDefault(sm => sm.Id == model.StaffMemberId);

        //    if (staffMember == null)
        //    {
        //        return NotFound();
        //    }

        //    var availableTimes = GetAvailableTimes(staffMember, model.SelectedDate);

        //    var createAppointmentViewModel = new CreateAppointmentViewModel
        //    {
        //        ServiceId = model.ServiceId,
        //        StaffMemberId = model.StaffMemberId,
        //        SelectedDate = model.SelectedDate,
        //        Services = services,
        //        StaffMembers = staffMembers,
        //        AvailableTimes = availableTimes.Select(at => new SelectListItem
        //        {
        //            Value = at.ToString(),
        //            Text = at.ToString(@"hh\:mm")
        //        }).ToList()
        //    };

        //    return View("SelectTimeSlot", createAppointmentViewModel);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(AppointmentViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Fetch the selected service and staff member from the database
        //        var service = await _context.Services.FindAsync(model.ServiceId);
        //        var staffMember = await _context.StaffMembers.FindAsync(model.StaffMemberId);

        //        if (service == null || staffMember == null)
        //        {
        //            return NotFound();
        //        }

        //        // Create the new appointment
        //        var appointment = new CreateAppointmentViewModel
        //        {
        //            Id = Guid.NewGuid(),
        //            UserId = Guid.Parse(_userManager.GetUserId(User)), // Get the UserId from the logged-in user
        //            ServiceId = service.Id,
        //            StaffMemberId = staffMember.Id,
        //            AppointmentDate = model.SelectedDate.Add(model.SelectedTime),
        //        };

        //        // Add the appointment to the database
        //        _context.Appointments.Add(appointment);
        //        await _context.SaveChangesAsync();

        //        return RedirectToAction("Index", "Appointment"); // Redirect to a confirmation or list of appointments
        //    }

        //    // If model validation fails, repopulate the Services and StaffMembers dropdowns and return the view
        //    var services = await _context.Services
        //        .Select(s => new SelectListItem
        //        {
        //            Value = s.Id.ToString(),
        //            Text = s.Name
        //        })
        //        .ToListAsync();

        //    var staffMembers = await _context.StaffMembers
        //        .Select(sm => new SelectListItem
        //        {
        //            Value = sm.Id.ToString(),
        //            Text = sm.Name
        //        })
        //        .ToListAsync();

        //    model.Services = services;
        //    model.StaffMembers = staffMembers;

        //    return View(model);
        //}

        // POST: Appointment/Save
        //[HttpPost]
        //public async Task<IActionResult> SaveAppointment(CreateAppointmentViewModel model)
        //{
        //    var services = _context.Services
        //       .Select(s => new SelectListItem
        //       {
        //           Value = s.Id.ToString(),
        //           Text = s.Name // Assuming `Name` is a property of Service
        //       })
        //       .ToList();

        //    var staffMembers = _context.StaffMembers
        //       .Select(sm => new SelectListItem
        //       {
        //           Value = sm.Id.ToString(),
        //           Text = sm.Name // Assuming `Name` is a property of StaffMember
        //       })
        //       .ToList();
        //    // Check if the selected date is valid
        //    if (model.SelectedDate < DateTime.Today)
        //    {
        //        ModelState.AddModelError("SelectedDate", "The selected date must be after today.");
        //        model.Services = _context.Services
        //            .Where(s => !s.IsDeleted)
        //            .Select(s => new SelectListItem
        //            {
        //                Value = s.Id.ToString(),
        //                Text = s.Name
        //            }).ToList();
        //        model.StaffMembers = _context.StaffMembers
        //            .Where(sm => !sm.IsDeleted)
        //            .Select(sm => new SelectListItem
        //            {
        //                Value = sm.Id.ToString(),
        //                Text = sm.Name
        //            }).ToList();
        //        return View(model);
        //    }

        //    // Fetch available times based on selected staff member and date
        //    var staffMember = _context.StaffMembers.FirstOrDefault(sm => sm.Id == model.StaffMemberId);

        //    if (staffMember == null)
        //    {
        //        return NotFound();
        //    }

        //    var availableTimes = GetAvailableTimes(staffMember, model.SelectedDate);

        //    var createAppointmentViewModel = new CreateAppointmentViewModel
        //    {
        //        ServiceId = model.ServiceId,
        //        StaffMemberId = model.StaffMemberId,
        //        SelectedDate = model.SelectedDate,
        //        Services = services,
        //        StaffMembers = staffMembers,
        //        AvailableTimes = availableTimes.Select(at => new SelectListItem
        //        {
        //            Value = at.ToString(),
        //            Text = at.ToString(@"hh\:mm")
        //        }).ToList()
        //    };
        //    if (ModelState.IsValid)
        //    {
        //        //var staffMember = _context.StaffMembers.FirstOrDefault(sm => sm.Id == model.StaffMemberId);
        //        var service = _context.Services.FirstOrDefault(s => s.Id == model.ServiceId);

        //        if (staffMember == null || service == null)
        //        {
        //            return NotFound();
        //        }

        //        var appointment = new Appointment
        //        {
        //            Id = Guid.NewGuid(),
        //            UserId = Guid.Parse(_userManager.GetUserId(User)),  // Convert string to Guid
        //            ServiceId = service.Id,
        //            StaffMemberId = staffMember.Id,
        //            AppointmentDate = model.SelectedDate.Add(model.SelectedTime),  // Add time to selected date
        //        };

        //        _context.Appointments.Add(appointment);
        //        await _context.SaveChangesAsync();

        //        return RedirectToAction("Index", "Appointment"); // Redirect to homepage or appointment confirmation page
        //    }

        //    return View(model);
        //}

        //// Helper method to get available time slots for a given staff member and date
        //private List<TimeSpan> GetAvailableTimes(StaffMember staffMember, DateTime selectedDate)
        //{
        //    var workStartTime = new TimeSpan(9, 0, 0); // Начало на работния ден (9:00 AM)
        //    var workEndTime = new TimeSpan(17, 0, 0);  // Край на работния ден (5:00 PM)
        //    var availableTimes = new List<TimeSpan>();

        //    // Преглеждаме времевите интервали (на всеки 30 минути)
        //    for (var time = workStartTime; time < workEndTime; time = time.Add(TimeSpan.FromMinutes(30)))
        //    {
        //        // Търсим наличен TimeSlot за съответния час, служител и дата
        //        var timeSlot = _context.TimeSlots
        //            .Where(ts => ts.StaffMemberId == staffMember.Id
        //                         && ts.Date.Date == selectedDate.Date // Сравняваме само датите (без час)
        //                         && ts.Time == time // Времето е равно на времето, което проверяваме
        //                         && !ts.IsBooked)  // Проверяваме дали часът не е зает
        //            .FirstOrDefault();  // Вземаме първия срещнат свободен час

        //        // Ако часът не е зает, добавяме го в списъка със свободни часове
        //        if (timeSlot == null)
        //        {
        //            // Ако няма зает час, добавяме този интервал в списъка с възможни часове
        //            availableTimes.Add(time);
        //        }
        //    }

        //    // Ако не са намерени свободни часове, може да върнем празен списък или съобщение
        //    return availableTimes;
        //}

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Get available services
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

        //POST: Save the appointment
        [HttpPost]
        public async Task<IActionResult> Create(AppointmentViewModel model)
        {
            //await PopulateDropdowns(model);
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

            // Assign available times to the model
            model.AvailableTimes = availableTimes.Select(at => new SelectListItem
            {
                Value = at.ToString(),
                Text = at.ToString(@"hh\:mm")
            }).ToList();
            //if (ModelState.IsValid)
            //{
            //    // Check if service and staff member are valid
            var staffMember = await _context.StaffMembers.FindAsync(model.StaffMemberId);
            var service = await _context.Services.FindAsync(model.ServiceId);

            if (staffMember == null || service == null)
            {
                ModelState.AddModelError("", "Selected service or staff member is invalid.");
                return View(model);
            }

            // Ensure the available time is selected
            var selectedTime = model.AvailableTimes?.FirstOrDefault()?.Value;
            if (string.IsNullOrEmpty(selectedTime))
            {
                ModelState.AddModelError("", "Please select a valid time.");
                return View(model);
            }

            // Combine the selected date and time
            var appointmentDate = model.SelectedDate.Date.Add(TimeSpan.Parse(selectedTime));
            var appointmentTime = TimeSpan.Parse(selectedTime);
            // Get the service duration (in minutes) from the selected service
            int serviceDuration = service.DurationInMinutes;
            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(_userManager.GetUserId(User)),
                ServiceId = service.Id,
                StaffMemberId = staffMember.Id,
                AppointmentDate = appointmentDate
            };
            //var timeSlot = new TimeSlot
            //{
            //    Id = Guid.NewGuid(),
            //    UserId = Guid.Parse(_userManager.GetUserId(User)),
            //    Date = appointmentDate.Date,  // Make sure to set only the date (no time yet)
            //    Time = TimeSpan.Parse(selectedTime),  // The time selected by the user (e.g., "09:00" as a string)
            //    IsBooked = true,
            //    ServiceId = service.Id,
            //    StaffMemberId = staffMember.Id
            //};
            List<TimeSlot> timeSlots = new List<TimeSlot>();

            for (int i = 0; i < serviceDuration / 30; i++) // assuming time slots are 30 minutes
            {
                var timeSlot = new TimeSlot
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.Parse(_userManager.GetUserId(User)),
                    Date = appointmentDate.Date,
                    Time = appointmentTime.Add(TimeSpan.FromMinutes(i * 30)),  // Increment the time by 30 minutes for each step
                    IsBooked = true,
                    ServiceId = service.Id,
                    StaffMemberId = staffMember.Id
                };

                timeSlots.Add(timeSlot);
            }

            // Save the time slots to the database (ensure you save them all together)
            foreach (var x in timeSlots)
            {
                _context.TimeSlots.Add(x);
                await _context.SaveChangesAsync();
            }

            // Add the time slot and the appointment to the context
            // _context.TimeSlots.Add(timeSlot);
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Appointment");
            //}

            // If model is not valid, repopulate the Services and StaffMembers and return the view with errors
            //var services = await _context.Services
            //    .Select(s => new SelectListItem
            //    {
            //        Value = s.Id.ToString(),
            //        Text = s.Name
            //    })
            //    .ToListAsync();

            //var staffMembers = await _context.StaffMembers
            //    .Select(sm => new SelectListItem
            //    {
            //        Value = sm.Id.ToString(),
            //        Text = sm.Name
            //    })
            //    .ToListAsync();

            //model.Services = services;
            //model.StaffMembers = staffMembers;

            //return View(model);
        }




        // Helper method to populate dropdowns
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

            // Get available time slots for the selected staff member and date
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
