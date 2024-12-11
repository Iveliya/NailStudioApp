using AutoMapper;
using NailStudio.Data.Models;
using NailStudioApp.Web.ViewModel.StaffMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Service.MappingProfile
{
    public class StaffMemberMappingProfile : Profile
    {
        public StaffMemberMappingProfile()
        {
            CreateMap<StaffMember, StaffMemberIndexViewModel>();
            CreateMap<AddStaffMemberFormModel, StaffMember>()
               .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<StaffMember, EditStaffMemberViewModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.PhotoUrl));

            CreateMap<EditStaffMemberViewModel, StaffMember>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.PhotoUrl));
            CreateMap<StaffMember, DeleteStaffMemberViewModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.PhotoUrl));


        }
    }
}
