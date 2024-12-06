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
        //private readonly IStaffMemberService staffMemberService;
        //private readonly IMapper mapper;

        //public StaffMemberController(IStaffMemberService staffMemberService, IMapper mapper)
        //{
        //    this.staffMemberService = staffMemberService;
        //    this.mapper = mapper;
        //}
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
            //var staffMembers = await this.staffMemberService.GetAllStaffMembersAsync();
            //return View(staffMembers);
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

                return RedirectToAction("Index");
            }

            return View(model);
        }

    }
}
