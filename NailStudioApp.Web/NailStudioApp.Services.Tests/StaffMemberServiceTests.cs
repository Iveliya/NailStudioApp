using AutoMapper;
using Moq;
using NailStudio.Data.Models;
using NailStudio.Data.Repository.Interfaces;
using NailStudioApp.Services.Data;
using NailStudioApp.Web.ViewModel.StaffMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Services.Tests
{
    [TestFixture]
    public class StaffMemberServiceTests
    {
        private NailStudioApp.Services.Data.StaffMemberService staffMemberService;

        private Mock<IRepository<StaffMember, Guid>> staffMemerRepositoryMock;
        private Mock<IMapper> mapperMock;

        [SetUp]
        public void Setup()
        {
            staffMemerRepositoryMock = new Mock<IRepository<StaffMember, Guid>>();
            mapperMock = new Mock<IMapper>();

            staffMemberService = new NailStudioApp.Services.Data.StaffMemberService(staffMemerRepositoryMock.Object, mapperMock.Object);

        }
       
        [Test]
        public async Task AddStaffMember_ShouldSucceed()
        {
            var model = new AddStaffMemberFormModel { Name = "John Doe", Role = "Nail Technician", PhotoUrl = "photo1.jpg" };
            var staffMember = new StaffMember { Id = Guid.NewGuid(), Name = "John Doe", Role = "Nail Technician", PhotoUrl = "photo1.jpg" };

            mapperMock.Setup(m => m.Map<StaffMember>(It.IsAny<AddStaffMemberFormModel>())).Returns(staffMember);
            staffMemerRepositoryMock.Setup(r => r.AddAsync(It.IsAny<StaffMember>())).Returns(Task.CompletedTask);
            staffMemerRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            await staffMemberService.AddStaffMemberAsync(model);

            staffMemerRepositoryMock.Verify(r => r.AddAsync(It.IsAny<StaffMember>()), Times.Once);
            staffMemerRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void AddStaffMember_ShouldThrowException()
        {
            var model = new AddStaffMemberFormModel { Name = "Jane Smith", Role = "Receptionist", PhotoUrl = "photo2.jpg" };

            staffMemerRepositoryMock.Setup(r => r.AddAsync(It.IsAny<StaffMember>())).Throws(new Exception("Repository error"));

            Assert.ThrowsAsync<Exception>(async () => await staffMemberService.AddStaffMemberAsync(model));
        }

        [Test]
        public void AddStaffMember_ShouldMapModelToStaffMember()
        {
            var model = new AddStaffMemberFormModel { Name = "Jane Smith", Role = "Receptionist", PhotoUrl = "photo2.jpg" };
            var staffMember = new StaffMember { Id = Guid.NewGuid(), Name = "Jane Smith", Role = "Receptionist", PhotoUrl = "photo2.jpg" };

            mapperMock.Setup(m => m.Map<StaffMember>(model)).Returns(staffMember);

            staffMemberService.AddStaffMemberAsync(model);

            mapperMock.Verify(m => m.Map<StaffMember>(model), Times.Once);
        }

        

        [Test]
        public async Task AddStaffMember_ShouldCallSaveChanges()
        {
            var model = new AddStaffMemberFormModel { Name = "Michael Jordan", Role = "Manager", PhotoUrl = "photo3.jpg" };
            var staffMember = new StaffMember { Id = Guid.NewGuid(), Name = "Michael Jordan", Role = "Manager", PhotoUrl = "photo3.jpg" };

            mapperMock.Setup(m => m.Map<StaffMember>(It.IsAny<AddStaffMemberFormModel>())).Returns(staffMember);
            staffMemerRepositoryMock.Setup(r => r.AddAsync(It.IsAny<StaffMember>())).Returns(Task.CompletedTask);
            staffMemerRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            await staffMemberService.AddStaffMemberAsync(model);

            staffMemerRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

      
        [Test]
        public void GetAllStaffMembers_ShouldThrowException()
        {
            staffMemerRepositoryMock.Setup(r => r.GetAllAttached()).Throws(new Exception("Repository error"));

            Assert.ThrowsAsync<Exception>(async () => await staffMemberService.GetAllStaffMembersAsync());
        }
        

        [Test]
        public void AddStaffMember_ShouldMapFormModel_WhenValid()
        {
            var model = new AddStaffMemberFormModel { Name = "Michael Jordan", Role = "Manager", PhotoUrl = "photo3.jpg" };
            var staffMember = new StaffMember { Id = Guid.NewGuid(), Name = "Michael Jordan", Role = "Manager", PhotoUrl = "photo3.jpg" };

            staffMemberService.AddStaffMemberAsync(model);

            mapperMock.Verify(m => m.Map<StaffMember>(model), Times.Once);
        }

        [Test]
        public async Task AddStaffMember_ShouldCallAdd_WhenValid()
        {
            var model = new AddStaffMemberFormModel { Name = "Sarah Williams", Role = "Assistant", PhotoUrl = "photo4.jpg" };
            var staffMember = new StaffMember { Id = Guid.NewGuid(), Name = "Sarah Williams", Role = "Assistant", PhotoUrl = "photo4.jpg" };

            mapperMock.Setup(m => m.Map<StaffMember>(It.IsAny<AddStaffMemberFormModel>())).Returns(staffMember);
            staffMemerRepositoryMock.Setup(r => r.AddAsync(It.IsAny<StaffMember>())).Returns(Task.CompletedTask);
            staffMemerRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            await staffMemberService.AddStaffMemberAsync(model);

            staffMemerRepositoryMock.Verify(r => r.AddAsync(It.IsAny<StaffMember>()), Times.Once);
        }

        [Test]
        public async Task AddStaffMember_ShouldCallSaveChanges_AddedSuccessfully()
        {
            var model = new AddStaffMemberFormModel { Name = "Emma Brown", Role = "Salon Manager", PhotoUrl = "photo5.jpg" };
            var staffMember = new StaffMember { Id = Guid.NewGuid(), Name = "Emma Brown", Role = "Salon Manager", PhotoUrl = "photo5.jpg" };

            mapperMock.Setup(m => m.Map<StaffMember>(It.IsAny<AddStaffMemberFormModel>())).Returns(staffMember);
            staffMemerRepositoryMock.Setup(r => r.AddAsync(It.IsAny<StaffMember>())).Returns(Task.CompletedTask);
            staffMemerRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            await staffMemberService.AddStaffMemberAsync(model);

            staffMemerRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task AddStaffMember_ShouldAddStaffMember_WhenValidModel()
        {
            var model = new AddStaffMemberFormModel { Name = "David Harris", Role = "Technician", PhotoUrl = "photo7.jpg" };
            var staffMember = new StaffMember { Id = Guid.NewGuid(), Name = "David Harris", Role = "Technician", PhotoUrl = "photo7.jpg" };

            mapperMock.Setup(m => m.Map<StaffMember>(It.IsAny<AddStaffMemberFormModel>())).Returns(staffMember);
            staffMemerRepositoryMock.Setup(r => r.AddAsync(It.IsAny<StaffMember>())).Returns(Task.CompletedTask);
            staffMemerRepositoryMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            await staffMemberService.AddStaffMemberAsync(model);

            staffMemerRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

       

    }
}
