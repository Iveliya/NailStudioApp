using Microsoft.AspNetCore.Identity;
using Moq;
using NailStudio.Data.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Services.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private NailStudioApp.Services.Data.UserService userService;
        private Mock<UserManager<User>> userManagerMock;
        private Mock<RoleManager<IdentityRole<Guid>>> roleManagerMock;

        [SetUp]
        public void Setup()
        {
            userManagerMock = new Mock<UserManager<User>>(
                new Mock<IUserStore<User>>().Object,
                null, null, null, null, null, null, null, null);

            roleManagerMock = new Mock<RoleManager<IdentityRole<Guid>>>(
                new Mock<IRoleStore<IdentityRole<Guid>>>().Object,
                null, null, null, null);

            userService = new Data.UserService(userManagerMock.Object, roleManagerMock.Object);
        }

        
        [Test]
        public async Task UserExistsByIdAsync_ShouldReturnTrue_WhenUserExists()
        {
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Email = "user@example.com" };

            userManagerMock.Setup(u => u.FindByIdAsync(userId.ToString())).ReturnsAsync(user);

            var result = await userService.UserExistsByIdAsync(userId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task UserExistsByIdAsync_ShouldReturnFalse_WhenUserDoesNotExist()
        {
            var userId = Guid.NewGuid();

            userManagerMock.Setup(u => u.FindByIdAsync(userId.ToString())).ReturnsAsync((User)null);

            var result = await userService.UserExistsByIdAsync(userId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task AssignUserToRoleAsync_ShouldReturnTrue_WhenRoleIsAssignedSuccessfully()
        {
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Email = "user@example.com" };
            var roleName = "Admin";

            userManagerMock.Setup(u => u.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
            roleManagerMock.Setup(r => r.RoleExistsAsync(roleName)).ReturnsAsync(true);
            userManagerMock.Setup(u => u.IsInRoleAsync(user, roleName)).ReturnsAsync(false);
            userManagerMock.Setup(u => u.AddToRoleAsync(user, roleName)).ReturnsAsync(IdentityResult.Success);

            var result = await userService.AssignUserToRoleAsync(userId, roleName);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task AssignUserToRoleAsync_ShouldReturnFalse_WhenRoleDoesNotExist()
        {
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Email = "user@example.com" };
            var roleName = "Admin";

            userManagerMock.Setup(u => u.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
            roleManagerMock.Setup(r => r.RoleExistsAsync(roleName)).ReturnsAsync(false);

            var result = await userService.AssignUserToRoleAsync(userId, roleName);

            Assert.IsFalse(result);
        }


        [Test]
        public async Task RemoveUserRoleAsync_ShouldReturnTrue_WhenRoleIsRemovedSuccessfully()
        {
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Email = "user@example.com" };
            var roleName = "Admin";

            userManagerMock.Setup(u => u.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
            roleManagerMock.Setup(r => r.RoleExistsAsync(roleName)).ReturnsAsync(true);
            userManagerMock.Setup(u => u.IsInRoleAsync(user, roleName)).ReturnsAsync(true);
            userManagerMock.Setup(u => u.RemoveFromRoleAsync(user, roleName)).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await userService.RemoveUserRoleAsync(userId, roleName);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task RemoveUserRoleAsync_ShouldReturnFalse_WhenRoleDoesNotExist()
        {
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Email = "user@example.com" };
            var roleName = "Admin";

            userManagerMock.Setup(u => u.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
            roleManagerMock.Setup(r => r.RoleExistsAsync(roleName)).ReturnsAsync(false);

            var result = await userService.RemoveUserRoleAsync(userId, roleName);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task DeleteUserAsync_ShouldReturnTrue_WhenUserIsDeletedSuccessfully()
        {
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Email = "user@example.com" };

            userManagerMock.Setup(u => u.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
            userManagerMock.Setup(u => u.DeleteAsync(user)).ReturnsAsync(IdentityResult.Success);

            var result = await userService.DeleteUserAsync(userId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteUserAsync_ShouldReturnFalse_WhenUserDoesNotExist()
        {
            var userId = Guid.NewGuid();

            userManagerMock.Setup(u => u.FindByIdAsync(userId.ToString())).ReturnsAsync((User)null);

            var result = await userService.DeleteUserAsync(userId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task DeleteUserAsync_ShouldReturnFalse_WhenDeletionFails()
        {
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Email = "user@example.com" };

            userManagerMock.Setup(u => u.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
            userManagerMock.Setup(u => u.DeleteAsync(user)).ReturnsAsync(IdentityResult.Failed());

            var result = await userService.DeleteUserAsync(userId);

            Assert.IsFalse(result);
        }
    }
}
