using AutoMapper;
using Moq;
using NailStudio.Data.Models;
using NailStudio.Data.Repository.Interfaces;
using NailStudioApp.Services.Data;
using NailStudioApp.Web.ViewModel.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Services.Tests
{
    [TestFixture]
    public class AppointmentServiceTests
    {
        private NailStudioApp.Services.Data.ApointmentService appointmentService;

        private Mock<IRepository<Appointment, Guid>> appointmentRepositotyMock;
        private Mock<IMapper> mapperMock;

        [SetUp]
        public void Setup()
        {
            appointmentRepositotyMock = new Mock<IRepository<Appointment, Guid>>();
            mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<AppointmentIndexViewModel>(It.IsAny<Appointment>()))
              .Returns((Appointment source) => new AppointmentIndexViewModel
              {
                  Id = source.Id,
                  AppointmentDate = source.AppointmentDate
              });
            appointmentService = new NailStudioApp.Services.Data.ApointmentService(appointmentRepositotyMock.Object, mapperMock.Object);

        }

        [Test]
        public void ThrowsException_WhenRepoFails()
        {
            appointmentRepositotyMock.Setup(r => r.GetAllAttached()).Throws(new Exception("Repository error"));
            var appointmentService = new ApointmentService(appointmentRepositotyMock.Object, mapperMock.Object);

            Assert.ThrowsAsync<Exception>(async () => await appointmentService.GetAllAppointmentsAsync());
        }
       
        [Test]
        public void ThrowsException_WhenRepoErrorOccurs()
        {
            appointmentRepositotyMock.Setup(r => r.GetAllAttached()).Throws(new Exception("Repository error"));

            Assert.ThrowsAsync<Exception>(async () => await appointmentService.GetAllAppointmentsAsync());
        }

        [Test]
        public void ThrowsArgumentNullException_WhenRepoReturnsNull()
        {
            appointmentRepositotyMock.Setup(r => r.GetAllAttached()).Returns((IQueryable<Appointment>)null);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await appointmentService.GetAllAppointmentsAsync());
        }

        
        [Test]
        public void ThrowsRepoException_OnError()
        {
            appointmentRepositotyMock.Setup(r => r.GetAllAttached()).Throws(new Exception("Repository error"));

            Assert.ThrowsAsync<Exception>(async () => await appointmentService.GetAllAppointmentsAsync());
        }

       

    }
}
