using AutoMapper;
using Moq;
using NailStudio.Data.Models;
using NailStudio.Data.Repository.Interfaces;
using NailStudioApp.Web.ViewModel.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Services.Tests
{
    [TestFixture]
    public class ScheduleServiceTests
    {
        private NailStudioApp.Services.Data.ScheduleService scheduleService;

        private Mock<IRepository<Schedule, Guid>> scheduleRepositotyMock;
        private Mock<IMapper> mapperMock;

        [SetUp]
        public void Setup()
        {
            scheduleRepositotyMock = new Mock<IRepository<Schedule, Guid>>();
            mapperMock = new Mock<IMapper>();
            scheduleService = new NailStudioApp.Services.Data.ScheduleService(scheduleRepositotyMock.Object, mapperMock.Object);

        }
       

        [Test]
        public async Task AddScheduleAsync_ShouldAddScheduleSuccessfully()
        {
            var model = new AddScheduleViewModel
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                StaffMemberId = Guid.NewGuid(),
                IsAvailable = true
            };

            var schedule = new Schedule
            {
                Id = Guid.NewGuid(),
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                StaffMemberId = model.StaffMemberId,
                IsAvailable = model.IsAvailable
            };

            mapperMock.Setup(m => m.Map<Schedule>(model)).Returns(schedule);
            scheduleRepositotyMock.Setup(r => r.AddAsync(It.IsAny<Schedule>())).Returns(Task.CompletedTask);
            scheduleRepositotyMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            await scheduleService.AddScheduleAsync(model);

            scheduleRepositotyMock.Verify(r => r.AddAsync(It.IsAny<Schedule>()), Times.Once);
            scheduleRepositotyMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void AddScheduleAsync_ShouldThrowException_WhenRepositoryFails()
        {
            // Arrange
            var model = new AddScheduleViewModel
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                StaffMemberId = Guid.NewGuid(),
                IsAvailable = true
            };

            scheduleRepositotyMock.Setup(r => r.AddAsync(It.IsAny<Schedule>())).Throws(new Exception("Repository error"));

            Assert.ThrowsAsync<Exception>(async () => await scheduleService.AddScheduleAsync(model));
        }

        [Test]
        public void AddScheduleAsync_ShouldMapViewModelToSchedule_WhenValidModelProvided()
        {
            var model = new AddScheduleViewModel
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                StaffMemberId = Guid.NewGuid(),
                IsAvailable = true
            };

            var schedule = new Schedule
            {
                Id = Guid.NewGuid(),
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                StaffMemberId = model.StaffMemberId,
                IsAvailable = model.IsAvailable
            };

            mapperMock.Setup(m => m.Map<Schedule>(model)).Returns(schedule);

            scheduleService.AddScheduleAsync(model);

            mapperMock.Verify(m => m.Map<Schedule>(model), Times.Once);
        }

        [Test]
        public async Task AddScheduleAsync_ShouldCallSaveChanges_WhenScheduleIsAdded()
        {
            var model = new AddScheduleViewModel
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                StaffMemberId = Guid.NewGuid(),
                IsAvailable = true
            };

            var schedule = new Schedule
            {
                Id = Guid.NewGuid(),
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                StaffMemberId = model.StaffMemberId,
                IsAvailable = model.IsAvailable
            };

            mapperMock.Setup(m => m.Map<Schedule>(It.IsAny<AddScheduleViewModel>())).Returns(schedule);
            scheduleRepositotyMock.Setup(r => r.AddAsync(It.IsAny<Schedule>())).Returns(Task.CompletedTask);
            scheduleRepositotyMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            await scheduleService.AddScheduleAsync(model);

            scheduleRepositotyMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
        
        [Test]
        public async Task AddScheduleAsync_ShouldCallRepositoryAddAsync()
        {
            var model = new AddScheduleViewModel
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                StaffMemberId = Guid.NewGuid(),
                IsAvailable = true
            };

            var schedule = new Schedule
            {
                Id = Guid.NewGuid(),
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                StaffMemberId = model.StaffMemberId,
                IsAvailable = model.IsAvailable
            };

            mapperMock.Setup(m => m.Map<Schedule>(model)).Returns(schedule);
            scheduleRepositotyMock.Setup(r => r.AddAsync(It.IsAny<Schedule>())).Returns(Task.CompletedTask);

            await scheduleService.AddScheduleAsync(model);

            scheduleRepositotyMock.Verify(r => r.AddAsync(It.IsAny<Schedule>()), Times.Once);
        }

        



    }
}
