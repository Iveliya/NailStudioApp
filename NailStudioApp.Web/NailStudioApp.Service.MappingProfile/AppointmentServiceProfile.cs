using AutoMapper;
using NailStudio.Data.Models;
using NailStudioApp.Web.ViewModel.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Service.MappingProfile
{
    using NailStudio.Data.Models;
    public class AppointmentServiceProfile:Profile
    {
        public AppointmentServiceProfile()
        {
            CreateMap<Appointment, AppointmentIndexViewModel>();
            CreateMap<AppointmentViewModel, Appointment>();
           //.ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service.Name))
           //.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
           //.ForMember(dest => dest.StaffMemberName, opt => opt.MapFrom(src => src.StaffMember.Name))
           //.ForMember(dest => dest.ServicePrice, opt => opt.MapFrom(src => src.Service.Price.ToString("C")));
        }
    }
}
