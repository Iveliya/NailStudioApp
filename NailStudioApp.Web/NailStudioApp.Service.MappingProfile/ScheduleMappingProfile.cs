using AutoMapper;
using NailStudio.Data.Models;
using NailStudioApp.Web.ViewModel.Schedule;
using NailStudioApp.Web.ViewModel.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Service.MappingProfile
{
    public class ScheduleMappingProfile:Profile
    {
        public ScheduleMappingProfile()
        {
            CreateMap<Schedule, IndexScheduleViewModel>();
            //CreateMap<Service, DetailServiceViewModel>();
            //CreateMap<Service, ServiceEditFormModel>().ReverseMap();
            CreateMap<AddScheduleViewModel, Schedule>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
