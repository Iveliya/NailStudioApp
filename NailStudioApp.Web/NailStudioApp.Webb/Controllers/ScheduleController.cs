using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NailStudio.Data;
using NailStudio.Data.Models;
using NailStudioApp.Web.ViewModel.Schedule;
using NailStudioApp.Web.ViewModel.Service;

namespace NailStudioApp.Webb.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly NailDbContext _context;
        private readonly IMapper _mapper;
        public ScheduleController(NailDbContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var schedules = await _context.Schedules
                .Where(s => !s.IsDeleted)
                .Include(s => s.StaffMember)
                .ToListAsync();

            var scheduleViewModels = _mapper.Map<List<IndexScheduleViewModel>>(schedules);

            return View(scheduleViewModels); 
        }

        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            var schedule = await _context.Schedules
                .ProjectTo<IndexScheduleViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return View(schedule);
        }

        public IActionResult Add()
        {
            var model = new AddScheduleViewModel
            {
                StaffMembers = _context.StaffMembers
                    .Where(sm => !sm.IsDeleted) 
                    .Select(sm => new SelectListItem
                    {
                        Value = sm.Id.ToString(),
                        Text = sm.Name
                    }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(AddScheduleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var schedule = new Schedule
                {
                    StaffMemberId = model.StaffMemberId,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    IsAvailable = model.IsAvailable
                };

                _context.Schedules.Add(schedule);
                _context.SaveChanges();

                return RedirectToAction("Manage");
            }

            model.StaffMembers = _context.StaffMembers
                .Where(sm => !sm.IsDeleted)  
                .Select(sm => new SelectListItem
                {
                    Value = sm.Id.ToString(),
                    Text = sm.Name
                }).ToList();

            return View(model); 
        }
        public IActionResult Edit(Guid id)
        {
            var schedule = _context.Schedules
                .Where(s => s.Id == id && !s.IsDeleted)
                .FirstOrDefault();

            if (schedule == null)
            {
                return NotFound();
            }

            var model = new EditScheuleViewModel
            {
                Id = schedule.Id,
                StaffMemberId = schedule.StaffMemberId,
                StartTime = schedule.StartTime,
                EndTime = schedule.EndTime,
                IsAvailable = schedule.IsAvailable,
                StaffMembers = _context.StaffMembers
                    .Where(sm => !sm.IsDeleted)  
                    .Select(sm => new SelectListItem
                    {
                        Value = sm.Id.ToString(),
                        Text = sm.Name
                    }).ToList()
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(EditScheuleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var schedule = _context.Schedules
                    .FirstOrDefault(s => s.Id == model.Id && !s.IsDeleted);

                if (schedule == null)
                {
                    return NotFound(); 
                }

                schedule.StaffMemberId = model.StaffMemberId;
                schedule.StartTime = model.StartTime;
                schedule.EndTime = model.EndTime;
                schedule.IsAvailable = model.IsAvailable;

                _context.SaveChanges(); 

                return RedirectToAction("Manage"); 
            }

            model.StaffMembers = _context.StaffMembers
                .Where(sm => !sm.IsDeleted) 
                .Select(sm => new SelectListItem
                {
                    Value = sm.Id.ToString(),
                    Text = sm.Name
                }).ToList();

            return View(model); 
        }
        public IActionResult Delete(Guid id)
        {
            var schedule = _context.Schedules
                .Where(s => s.Id == id && !s.IsDeleted)
                .Include(s => s.StaffMember) 
                .FirstOrDefault();

            if (schedule == null)
            {
                return NotFound();
            }

            if (schedule.StaffMember == null)
            {
                return NotFound();
            }

            var model = new DeleteScheduleViewModel
            {
                Id = schedule.Id,
                StaffMemberName = schedule.StaffMember.Name, 
                StartTime = schedule.StartTime,
                EndTime = schedule.EndTime,
                IsAvailable = schedule.IsAvailable
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult Delete(DeleteScheduleViewModel model)
        {
            var schedule = _context.Schedules
                .Where(s => s.Id == model.Id && !s.IsDeleted)
                .FirstOrDefault();

            if (schedule == null)
            {
                return NotFound(); 
            }

            schedule.IsDeleted = true;

            _context.SaveChanges(); 

            return RedirectToAction("Manage");
        }





    }
}
