using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NailStudio.Data.Models;
using NailStudio.Data.Repository.Interfaces;
using NailStudioApp.Services.Data.Interfaces;
using NailStudioApp.Services.Mapping;
using NailStudioApp.Web.ViewModel.Review;
using NailStudioApp.Web.ViewModel.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Services.Data
{
    public class ReviewService:IReviewService
    {
        private readonly IRepository<Review, Guid> reviewRepository;
        private readonly IMapper mapper;

        public ReviewService(IRepository<Review, Guid> reviewRepository, IMapper mapper)
        {
            this.reviewRepository = reviewRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<IndexReviewViewModel>> GetAllReviewsAsync()
        {
              IEnumerable<IndexReviewViewModel> review = await this.reviewRepository
                .GetAllAttached()
               .To<IndexReviewViewModel>()
               .ToListAsync();
            return review;
        }

        public async Task AddReviewAsync(AddReviewViewModel model)
        {
            var review = this.mapper.Map<Review>(model);

            await this.reviewRepository.AddAsync(review);

            await this.reviewRepository.SaveChangesAsync();
        }
    }
}
