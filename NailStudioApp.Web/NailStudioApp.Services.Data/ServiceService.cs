using AutoMapper;
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
        private readonly IMapper mapper;
        public ServiceService(IRepository<Service, Guid> serviceRepository, IMapper mapper)
        {
            this.serviceRepository = serviceRepository;
            this.mapper = mapper;
        }
        public async Task AddServiceAsync(AddServiceFormModel model)
        {
            var service = this.mapper.Map<Service>(model);

            await this.serviceRepository.AddAsync(service);

            await this.serviceRepository.SaveChangesAsync();
        }

        public async Task<DetailServiceViewModel> GetServiceDetailsByIdAsync(Guid id)
        {
            var service = await this.serviceRepository
        .All()  
        .Where(s => s.Id == id)  
        .FirstOrDefaultAsync();  

            if (service == null)
            {
                return null;  
            }
            var serviceViewModel = this.mapper.Map<DetailServiceViewModel>(service);

            return serviceViewModel;
            //var service = await this.serviceRepository.GetByIdAsync(id);
            //if (service == null)
            //{
            //    return null;  
            //}

            //return this.mapper.Map<DetailServiceViewModel>(service);
        }

        public async Task<IEnumerable<ServiceIndexViewModel>> IndexGetAllOrderedAsync()
        {
            IEnumerable<ServiceIndexViewModel> services = await this.serviceRepository
                .GetAllAttached()
               .To<ServiceIndexViewModel>()
               .ToListAsync();
            return services;

            //var services = await this.serviceRepository
            //    .GetAll()
            //    .To<ServiceIndexViewModel>()  
            //    .ToListAsync();  
            //return services;
        }
    }
}
