using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NailStudio.Data;
using NailStudio.Data.Models;
using NailStudioApp.Services.Data.Interfaces;
using NailStudioApp.Web.ViewModel.Service;
using NailStudioApp.Web.ViewModel.StaffMember;

namespace NailStudioApp.Webb.Controllers
{
    public class StaffMemberController : Controller
    {
        
        private readonly NailDbContext _context;
        private readonly IMapper _mapper;
        public StaffMemberController(NailDbContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var staffMemberViewModel = await _context.StaffMembers
        .ProjectTo<StaffMemberIndexViewModel>(_mapper.ConfigurationProvider)
        .ToListAsync();

            return View(staffMemberViewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStaffMemberFormModel model)
        {
           


            if (ModelState.IsValid)
            {
                var staffMember = _mapper.Map<StaffMember>(model);

                await _context.StaffMembers.AddAsync(staffMember);
                await _context.SaveChangesAsync();

                return RedirectToAction("Manage");
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            var staffMmember = await _context.StaffMembers
                .ProjectTo<StaffMemberIndexViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return View(staffMmember);
        }

        public IActionResult Edit(Guid id)
        {
            var staffMember = _context.StaffMembers
                .FirstOrDefault(s => s.Id == id);

            if (staffMember == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<EditStaffMemberViewModel>(staffMember);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditStaffMemberViewModel model)
        {
            if (ModelState.IsValid)
            {
                var staffMember = _context.StaffMembers
                    .FirstOrDefault(s => s.Id == model.Id);

                if (staffMember == null)
                {
                    return NotFound(); 
                }

                _mapper.Map(model, staffMember);

                _context.SaveChanges();
                return RedirectToAction("Manage", "StaffMember");
            }
            return View(model);
        }
        public IActionResult Delete(Guid id)
        {
            var staffMember = _context.StaffMembers
                .Where(s => s.Id == id && !s.IsDeleted)
                .FirstOrDefault();

            if (staffMember == null)
            {
                return NotFound(); // If the staff member is not found, return NotFound
            }

            // Map to DeleteStaffMemberViewModel
            var model = _mapper.Map<DeleteStaffMemberViewModel>(staffMember);

            return View(model); // Return the view with the model
        }

        // POST: StaffMember/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(DeleteStaffMemberViewModel model)
        {
            var staffMember = _context.StaffMembers
                .FirstOrDefault(s => s.Id == model.Id);

            if (staffMember == null)
            {
                return NotFound(); // If the staff member is not found, return NotFound
            }

            // Perform the "soft delete" by marking the staff member as deleted
            staffMember.IsDeleted = true;

            // Save the changes to the database
            _context.SaveChanges();

            // Redirect to the manage page after successful deletion
            return RedirectToAction("Manage", "StaffMember");
        }
    }
}
