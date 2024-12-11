using AutoMapper;
using NailStudio.Data.Models;
using NailStudio.Data.Repository.Interfaces;
using NailStudioApp.Services.Data.Interfaces;
using NailStudioApp.Web.ViewModel.Schedule;
using AutoMapper;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;
using NailStudio.Data.Models;
using NailStudio.Data.Repository.Interfaces;
using NailStudioApp.Services.Data.Interfaces;
using NailStudioApp.Services.Mapping;
using NailStudioApp.Web.ViewModel.Service;
using NailStudioApp.Web.ViewModel.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Services.Data
{
    public class ScheduleService:IScheduleService
    {
        private IRepository<Schedule, Guid> scheduleRepository;
        private readonly IMapper mapper;
        public ScheduleService(IRepository<Schedule, Guid> scheduleRepository, IMapper mapper)
        {
            this.scheduleRepository = scheduleRepository;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<IndexScheduleViewModel>> AllScheduleAsync()
        {
            IEnumerable<IndexScheduleViewModel> schedule = await this.scheduleRepository
                .GetAllAttached()
               .To<IndexScheduleViewModel>()
               .ToListAsync();
            return schedule;

        }

        public async Task AddScheduleAsync(AddScheduleViewModel model)
        {
            var schedule = this.mapper.Map<Schedule>(model);

            await this.scheduleRepository.AddAsync(schedule);

            await this.scheduleRepository.SaveChangesAsync();
        }

    }
}
