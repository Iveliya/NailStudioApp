using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NailStudio.Data.Models;
using NailStudio.Data.Repository.Interfaces;
using NailStudioApp.Services.Data.Interfaces;
using NailStudioApp.Services.Mapping;
using NailStudioApp.Web.ViewModel.Service;
using NailStudioApp.Web.ViewModel.StaffMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Services.Data
{
    public class StaffMemberService : IStaffMemberService
    {
        private readonly IRepository<StaffMember, Guid> staffRepository;
        private readonly IMapper mapper;

        public StaffMemberService(IRepository<StaffMember, Guid> staffRepository, IMapper mapper)
        {
            this.staffRepository = staffRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<StaffMemberIndexViewModel>> GetAllStaffMembersAsync()
        {
            //return await this.staffRepository
            //    .All()
            //    .OrderBy(sm => sm.Name)
            //    .To<StaffMemberIndexViewModel>()
            //    .ToListAsync();

            IEnumerable<StaffMemberIndexViewModel> staffMembers = await this.staffRepository
                .GetAllAttached()
               .To<StaffMemberIndexViewModel>()
               .ToListAsync();
            return staffMembers;
        }
        public async Task AddStaffMemberAsync(AddStaffMemberFormModel model)
        {
            var staffMember = this.mapper.Map<StaffMember>(model);

            await this.staffRepository.AddAsync(staffMember);

            await this.staffRepository.SaveChangesAsync();
        }
    }
}
