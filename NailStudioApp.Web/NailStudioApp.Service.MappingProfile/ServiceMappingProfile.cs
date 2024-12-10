using AutoMapper;
using NailStudioApp.Web.ViewModel.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Service.MappingProfile
{
    using NailStudio.Data.Models;
    public class ServiceMappingProfile:Profile
    {

        public ServiceMappingProfile()
        {
            CreateMap<Service, ServiceIndexViewModel>();
            CreateMap<Service, DetailServiceViewModel>();
            CreateMap<Service, ServiceEditFormModel>().ReverseMap();
            CreateMap<AddServiceFormModel, Service>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
