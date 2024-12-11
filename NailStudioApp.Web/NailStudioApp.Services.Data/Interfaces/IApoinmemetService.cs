using NailStudioApp.Web.ViewModel.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Services.Data.Interfaces
{
    public interface IApoinmemetService
    {
        Task<IEnumerable<AppointmentIndexViewModel>> GetAllAppointmentsAsync();
    }
}
