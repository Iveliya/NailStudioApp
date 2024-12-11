using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NailStudio.Data;
using NailStudio.Data.Models;
using NailStudioApp.Services.Data;
using NailStudioApp.Services.Data.Interfaces;
using NailStudioApp.Web.ViewModel.Appointment;
using NailStudioApp.Web.ViewModel.Review;
using NailStudioApp.Web.ViewModel.Service;

namespace NailStudioApp.Webb.Controllers
{
    public class ReviewController : Controller
    {
        private readonly NailDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public ReviewController(NailDbContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            this._mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var reviewViewModels = await _context.Reviews
        .ProjectTo<IndexReviewViewModel>(_mapper.ConfigurationProvider)
        .ToListAsync();

            return View(reviewViewModels);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Add(AddReviewViewModel model)
        {
            if (ModelState.IsValid)
            {
                var review = new Review
                {
                   
                    Content = model.Content,
                    Rating = model.Rating,
                    UserId = Guid.Parse(_userManager.GetUserId(User)),
                    Date= DateTime.Now
                };

                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Service");  
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            var reviews = await _context.Reviews
                .ProjectTo<IndexReviewViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return View(reviews);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var review = await _context.Reviews
                                        .Where(r => r.Id == id && !r.IsDeleted)
                                        .FirstOrDefaultAsync();

            if (review == null)
            {
                return NotFound(); 
            }

            var reviewViewModel = _mapper.Map<Review, IndexReviewViewModel>(review);

            return View(reviewViewModel);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var review = await _context.Reviews
                                        .Where(r => r.Id == id && !r.IsDeleted)
                                        .FirstOrDefaultAsync();

            if (review == null)
            {
                return NotFound(); 
            }

            review.IsDeleted = true;
            _context.Reviews.Update(review);  
            await _context.SaveChangesAsync(); 

            return RedirectToAction(nameof(Manage));
        }

    }
}
