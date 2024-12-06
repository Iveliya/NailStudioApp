using AutoMapper;
using NailStudio.Data.Models;
using NailStudioApp.Web.ViewModel.StaffMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Services.Mapping.Mapping
{
    public class StaffMemberMappingProfile:Profile
    {
        public StaffMemberMappingProfile()
        {
            CreateMap<StaffMember, StaffMemberIndexViewModel>();
            //CreateMap<StaffMember, DetailStaffMemberViewModel>();
            CreateMap<AddStaffMemberFormModel, StaffMember>()
               .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
