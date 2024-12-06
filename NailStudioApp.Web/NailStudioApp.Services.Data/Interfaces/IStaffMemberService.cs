using NailStudioApp.Web.ViewModel.StaffMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Services.Data.Interfaces
{
    public interface IStaffMemberService
    {
        Task<IEnumerable<StaffMemberIndexViewModel>> GetAllStaffMembersAsync();
        Task AddStaffMemberAsync(AddStaffMemberFormModel model);
        //Task<DetailStaffMemberViewModel> GetStaffMemberByIdAsync(Guid id);
    }
}
