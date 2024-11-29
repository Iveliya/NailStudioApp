using Microsoft.AspNetCore.Mvc;
using NailStudioApp.Data;
using NailStudioApp.Data.Models;
using NailStudioApp.Web.ViewModels.Appointment;

namespace NailStudioApp.Web.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly NailStudioDbContext dbContext;
        public AppointmentController(NailStudioDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public IActionResult Index()
        {
            IEnumerable<AppointmentIndexViewModel> appointment = this.dbContext
                .Appointments
                .Select(a=>new AppointmentIndexViewModel()
                {
                    Id = a.Id,
                    ClientName=a.Client.FirstName,
                    EmployeeName=a.Employee.FirstName,
                    AppointmentDate=a.AppointmentDate,
                    TotalPrice=a.TotalPrice,
                    Status=a.Status
                })
                .ToArray();

            return View(appointment);
        }
    }
}
