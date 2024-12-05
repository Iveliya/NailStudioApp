using NailStudioApp.Web.ViewModel.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Services.Data.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceIndexViewModel>> IndexGetAllOrderedAsync();
        Task AddServiceAsync(AddServiceFormModel model);
        Task<DetailServiceViewModel> GetServiceDetailsByIdAsync(Guid id);
    }
}
