using AutoMapper;
using NailStudio.Data.Models;
using NailStudio.Data.Repository.Interfaces;
using NailStudioApp.Services.Data.Interfaces;
using NailStudioApp.Web.ViewModel.Appointment;
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
    using Microsoft.EntityFrameworkCore;
    using NailStudio.Data.Models;
    using NailStudio.Data.Repository.Interfaces;
    using NailStudioApp.Services.Data.Interfaces;
    using NailStudioApp.Services.Mapping;
    using NailStudioApp.Web.ViewModel.Service;
    public class ApointmentService : IApoinmemetService
    {
        private readonly IRepository<Appointment, Guid> appointmentRepository;
        private readonly IMapper mapper;

        public ApointmentService(IRepository<Appointment, Guid> appointmentRepository, IMapper mapper)
        {
            this.appointmentRepository = appointmentRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<AppointmentIndexViewModel>> GetAllAppointmentsAsync()
        {
            IEnumerable<AppointmentIndexViewModel> appointment = await this.appointmentRepository
                .GetAllAttached()
               .To<AppointmentIndexViewModel>()
               .ToListAsync();
            return appointment;
        }
        public async Task<Appointment> GetAppointmentByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id), "Appointment ID cannot be empty.");
            }

            var appointment = await this.appointmentRepository
                .GetAllAttached()
                .FirstOrDefaultAsync(a => a.Id == id);

            return appointment;
        }
    }
}
