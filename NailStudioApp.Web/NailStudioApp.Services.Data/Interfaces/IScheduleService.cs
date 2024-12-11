using NailStudioApp.Web.ViewModel.Admin.UserManagement;
using NailStudioApp.Web.ViewModel.Schedule;
using NailStudioApp.Web.ViewModel.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Services.Data.Interfaces
{
    public interface IScheduleService
    {
        Task<IEnumerable<IndexScheduleViewModel>> AllScheduleAsync();
        Task AddScheduleAsync(AddScheduleViewModel model);
    }
}
