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
                    .Where(sm => !sm.IsDeleted)  // Exclude deleted staff members
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

                return RedirectToAction("Manage"); // Redirect to manage page
            }

            model.StaffMembers = _context.StaffMembers
                .Where(sm => !sm.IsDeleted)  // Exclude deleted staff members
                .Select(sm => new SelectListItem
                {
                    Value = sm.Id.ToString(),
                    Text = sm.Name
                }).ToList();

            return View(model); // Return the model if validation fails
        }
        public IActionResult Edit(Guid id)
        {
            var schedule = _context.Schedules
                .Where(s => s.Id == id && !s.IsDeleted)
                .FirstOrDefault();

            if (schedule == null)
            {
                return NotFound(); // If the schedule doesn't exist
            }

            var model = new EditScheuleViewModel
            {
                Id = schedule.Id,
                StaffMemberId = schedule.StaffMemberId,
                StartTime = schedule.StartTime,
                EndTime = schedule.EndTime,
                IsAvailable = schedule.IsAvailable,
                StaffMembers = _context.StaffMembers
                    .Where(sm => !sm.IsDeleted)  // Exclude deleted staff members
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
                    return NotFound(); // If the schedule doesn't exist
                }

                // Update schedule properties
                schedule.StaffMemberId = model.StaffMemberId;
                schedule.StartTime = model.StartTime;
                schedule.EndTime = model.EndTime;
                schedule.IsAvailable = model.IsAvailable;

                _context.SaveChanges(); // Save changes to the database

                return RedirectToAction("Manage"); // Redirect to the schedule list page
            }

            // If validation fails, re-populate the staff members list and return to the form
            model.StaffMembers = _context.StaffMembers
                .Where(sm => !sm.IsDeleted)  // Exclude deleted staff members
                .Select(sm => new SelectListItem
                {
                    Value = sm.Id.ToString(),
                    Text = sm.Name
                }).ToList();

            return View(model); // Return the model if validation fails
        }
        public IActionResult Delete(Guid id)
        {
            // Eagerly load the related StaffMember to ensure it's not null
            var schedule = _context.Schedules
                .Where(s => s.Id == id && !s.IsDeleted)
                .Include(s => s.StaffMember) // Include the StaffMember so it is loaded
                .FirstOrDefault();

            if (schedule == null)
            {
                return NotFound(); // If the schedule doesn't exist
            }

            // Check if the StaffMember is null, which should no longer happen
            if (schedule.StaffMember == null)
            {
                return NotFound(); // Handle the case where StaffMember is missing
            }

            var model = new DeleteScheduleViewModel
            {
                Id = schedule.Id,
                StaffMemberName = schedule.StaffMember.Name, // Ensure staff member name is valid
                StartTime = schedule.StartTime,
                EndTime = schedule.EndTime,
                IsAvailable = schedule.IsAvailable
            };

            return View(model); // Return the Delete view with the schedule details
        }


        [HttpPost]
        public IActionResult Delete(DeleteScheduleViewModel model)
        {
            var schedule = _context.Schedules
                .Where(s => s.Id == model.Id && !s.IsDeleted)
                .FirstOrDefault();

            if (schedule == null)
            {
                return NotFound(); // If the schedule doesn't exist
            }

            // Soft delete the schedule by setting the IsDeleted flag to true
            schedule.IsDeleted = true;

            _context.SaveChanges(); // Save changes to the database

            return RedirectToAction("Manage"); // Redirect to the schedule list page
        }





    }
}
