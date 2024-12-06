using AutoMapper;
using NailStudio.Data.Models;
using NailStudioApp.Web.ViewModel.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Services.Mapping.Mapping
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            CreateMap<Service, ServiceIndexViewModel>();
            CreateMap<Service, DetailServiceViewModel>();

            CreateMap<AddServiceFormModel, Service>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
