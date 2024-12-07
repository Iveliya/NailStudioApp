using AutoMapper;
using NailStudio.Data.Models;
using NailStudioApp.Web.ViewModel.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Services.Mapping.Mapping
{
    public class ReviewMappingProfile:Profile
    {
        public ReviewMappingProfile()
        {
            CreateMap<Review, IndexReviewViewModel>();
            CreateMap<AddReviewViewModel, Review>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Date, opt => opt.MapFrom(_ => DateTime.UtcNow));
        }
    }
}
