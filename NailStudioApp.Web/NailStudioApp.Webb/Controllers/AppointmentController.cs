using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NailStudio.Data;
using NailStudioApp.Web.ViewModel.Appointment;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using NailStudio.Data.Models;
using Microsoft.AspNetCore.Identity;
using NailStudioApp.Web.Infrastruction.Extensions;
using static NailStudioApp.Web.ViewModel.Appointment.AddAppointmentViewModel;

namespace NailStudioApp.Webb.Controllers
{
    using static NailStudioApp.Web.ViewModel.Appointment.AddAppointmentViewModel;
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
        [HttpGet]
        public async Task<JsonResult> GetAvailableTimes(Guid staffMemberId, DateTime date)
        {
            // Проверка дали е неработен ден (неделя)
            if (date.DayOfWeek == DayOfWeek.Sunday)
            {
                return Json(new { error = "Не се работи в неделя." });
            }

            // Работно време от 9:00 до 17:00
            var allTimes = new List<TimeSpan>();
            for (var hour = 9; hour < 17; hour++)
            {
                allTimes.Add(new TimeSpan(hour, 0, 0)); // 9:00, 10:00 и т.н.
                allTimes.Add(new TimeSpan(hour, 30, 0)); // 9:30, 10:30 и т.н.
            }

            // Взимаме запазените часове за избрания служител и дата
            var bookedTimes = await _context.Appointments
                .Where(a => a.StaffMemberId == staffMemberId
                            && a.AppointmentDate.Date == date.Date
                            && !a.IsDeleted)
                .Select(a => a.AppointmentDate.TimeOfDay)
                .ToListAsync();

            // Филтрираме свободните часове
            var availableTimes = allTimes
                .Where(t => !bookedTimes.Contains(t))
                .Select(t => new
                {
                    Text = t.ToString(@"hh\:mm"),
                    Value = t.ToString(@"hh\:mm")
                })
                .ToList();

            return Json(new { availableTimes });
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new AddAppointmentViewModel
            {
                AvailableStaffMembers = await _context.StaffMembers
                    .Select(s => new SelectListItem
                    {
                        Text = s.Name,
                        Value = s.Id.ToString()
                    })
                    .ToListAsync(),

                AvailableServices = await _context.Services
                    .Select(s => new SelectListItem
                    {
                        Text = s.Name,
                        Value = s.Id.ToString()
                    })
                    .ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddAppointmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Вземане на текущия потребител
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                // Проверка дали срещата вече е запазена
                var existingAppointment = await _context.Appointments
                    .FirstOrDefaultAsync(a => a.StaffMemberId == model.StaffMemberId
                                              && a.AppointmentDate == model.AppointmentDate
                                              && !a.IsDeleted);

                if (existingAppointment != null)
                {
                    ModelState.AddModelError("", "Този час вече е зает.");
                    return View(model);
                }

                // Създаване на нова среща
                var appointment = new Appointment
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    ServiceId = model.ServiceId,
                    AppointmentDate = model.AppointmentDate,
                    StaffMemberId = model.StaffMemberId,
                    IsDeleted = false
                };

                // Записване на срещата в базата данни
                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
