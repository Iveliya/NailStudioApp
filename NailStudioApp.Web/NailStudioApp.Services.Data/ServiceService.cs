using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;
using NailStudio.Data.Models;
using NailStudio.Data.Repository.Interfaces;
using NailStudioApp.Services.Data.Interfaces;
using NailStudioApp.Services.Mapping;
using NailStudioApp.Web.ViewModel.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Services.Data
{
    public class ServiceService : IServiceService
    {
        private IRepository<Service, Guid> serviceRepository;
        public ServiceService(IRepository<Service, Guid> serviceRepository)
        {
            this.serviceRepository = serviceRepository;
        }
        public Task AddServiceAsync(AddServiceFormModel model)
        {
            throw new NotImplementedException();
        }

        public Task<DetailServiceViewModel> GetServiceDetailsByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ServiceIndexViewModel>> IndexGetAllOrderedAsync()
        {
            IEnumerable<ServiceIndexViewModel> services = await this.serviceRepository
                .GetAllAttached()
               .To<ServiceIndexViewModel>()
               .ToListAsync();
            return services;
        }
    }
}
