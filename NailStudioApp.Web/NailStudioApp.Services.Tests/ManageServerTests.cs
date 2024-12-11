using Microsoft.EntityFrameworkCore;
using Moq;
using NailStudio.Data.Models;
using NailStudio.Data.Repository.Interfaces;
using NailStudioApp.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Services.Tests
{
    [TestFixture]
    public class ManageServerTests
    {
        private ManagerService managerService;
        private Mock<IRepository<Manager, Guid>> managersRepositoryMock;

        [SetUp]
        public void Setup()
        {
            managersRepositoryMock = new Mock<IRepository<Manager, Guid>>();
            managerService = new ManagerService(managersRepositoryMock.Object);
        }


        [Test]
        public async Task IsUserManagerAsync_ShouldReturnFalse_WhenUserIdIsNullOrEmpty()
        {
            // Arrange
            string userId = string.Empty;  // Simulate an empty or null userId
            var managers = new[]
            {
            new Manager { UserId = Guid.NewGuid() }
        };

            // Mock the behavior of GetAllAttached to return a valid IQueryable
            managersRepositoryMock.Setup(m => m.GetAllAttached())
                .Returns(managers.AsQueryable());

            // Act
            var result = await managerService.IsUserManagerAsync(userId);

            // Assert
            Assert.IsFalse(result);
        }


        [Test]
        public async Task IsUserManagerAsync_ShouldReturnFalse_WhenUserIdIsEmptyString()
        {
            // Arrange
            var userId = string.Empty; // Empty userId
            var managers = new[]
            {
            new Manager { UserId = Guid.NewGuid() }
        };

            // Mock repository to return list of managers
            managersRepositoryMock.Setup(r => r.GetAllAttached())
                .Returns(managers.AsQueryable());

            // Act
            var result = await managerService.IsUserManagerAsync(userId);

            // Assert
            Assert.IsFalse(result);  // Empty userId should return false
        }

       
    }

}
