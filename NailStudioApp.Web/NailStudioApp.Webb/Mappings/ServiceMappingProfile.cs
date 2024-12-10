using AutoMapper;
using NailStudio.Data.Models;
using NailStudioApp.Web.ViewModel.Service;

namespace NailStudioApp.Webb.Mappings
{
    using NailStudio.Data.Models;
    public class ServiceMappingProfile:Profile
    {
        
        public ServiceMappingProfile()
        {
            CreateMap<Service, ServiceIndexViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.DurationInMinutes, opt => opt.MapFrom(src => src.DurationInMinutes))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));
        }
    }
}
