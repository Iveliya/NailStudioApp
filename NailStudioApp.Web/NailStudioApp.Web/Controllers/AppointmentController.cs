using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Abstractions;
//using NailStudioApp.Data;
//using NailStudioApp.Data.Models;
//using NailStudioApp.Web.ViewModels.Appointment;

namespace NailStudioApp.Web.Controllers
{
    public class AppointmentController : Controller
    {
        //private readonly NailStudioDbContext dbContext;
        //public AppointmentController(NailStudioDbContext dbContext)
        //{
        //    this.dbContext = dbContext;
        //}
        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //    IEnumerable<AppointmentIndexViewModel> appointment = await this.dbContext
        //        .Appointments
        //        .Select(a=>new AppointmentIndexViewModel()
        //        {
        //            Id = a.Id,
        //            EmployeeName=a.Employee.FirstName,
        //            AppointmentDate=a.AppointmentDate,
        //            TotalPrice=a.TotalPrice,
        //            Status=a.Status
        //        })
        //        .ToArrayAsync();

        //    return View(appointment);

        //}
        //[HttpGet]
        //public async  Task<IActionResult> Create()
        //{
            

        //    ViewBag.Employees = await dbContext.Employees
        //        .Select(e => new SelectListItem
        //        {
        //            Value = e.Id.ToString(),
        //            Text = e.FirstName + " " + e.LastName 
        //        })
        //        .ToListAsync();

        //    return this.View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> Create(AddAppointmentFormModel model)
        //{
        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.View(model);
        //    }
        //    Appointment appointment = new Appointment()
        //    {
        //        EmployeeId=model.EmployeeId,
        //        AppointmentDate = model.AppointmentDate,
        //        TotalPrice=model.TotalPrice,
        //        Status=model.Status
        //    };
        //    await this.dbContext.Appointments.AddAsync(appointment);
        //    await this.dbContext.SaveChangesAsync();
        //    return this.RedirectToAction(nameof(Index));
        //}
    }
}
