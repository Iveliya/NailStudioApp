using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NailStudio.Data;
using NailStudio.Data.Models;
using NailStudioApp.Services.Data.Interfaces;
using NailStudioApp.Web.ViewModel.Review;
using NailStudioApp.Web.ViewModel.Service;

namespace NailStudioApp.Webb.Controllers
{
    public class ReviewController : Controller
    {
        private readonly NailDbContext _context;
        private readonly IMapper _mapper;
        public ReviewController(NailDbContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var reviews = await this.reviewService.GetAllReviewsAsync();
            //return View(reviews);
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
        public async Task<IActionResult> Add(AddReviewViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    await this.reviewService.AddReviewAsync(model);
            //    return RedirectToAction("Index");
            //}

            //return View(model);
            if (ModelState.IsValid)
            {
                var review = _mapper.Map<Review>(model);

                await _context.Reviews.AddAsync(review);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
