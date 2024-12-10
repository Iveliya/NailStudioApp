using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NailStudio.Data.Models;
using NailStudioApp.Services.Data.Interfaces;
using NailStudioApp.Web.ViewModel.Admin.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Services.Data
{
    public class UserService:BaseService,IUserService
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;

        public UserService(UserManager<User> userManager,
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IEnumerable<AllUsersViewModel>> GetAllUsersAsync()
        {
            IEnumerable<User> allUsers = await this.userManager.Users
                .ToArrayAsync();
            ICollection<AllUsersViewModel> allUsersViewModel = new List<AllUsersViewModel>();

            foreach (User user in allUsers)
            {
                IEnumerable<string> roles = await this.userManager.GetRolesAsync(user);

                allUsersViewModel.Add(new AllUsersViewModel()
                {
                    Id = user.Id.ToString(),
                    Email = user.Email,
                    Roles = roles
                });
            }

            return allUsersViewModel;
        }

        public async Task<bool> UserExistsByIdAsync(Guid userId)
        {
            User? user = await this.userManager
                .FindByIdAsync(userId.ToString());

            return user != null;
        }

        public async Task<bool> AssignUserToRoleAsync(Guid userId, string roleName)
        {
            User? user = await userManager
                .FindByIdAsync(userId.ToString());
            bool roleExists = await this.roleManager.RoleExistsAsync(roleName);

            if (user == null || !roleExists)
            {
                return false;
            }

            bool alreadyInRole = await this.userManager.IsInRoleAsync(user, roleName);
            if (!alreadyInRole)
            {
                IdentityResult? result = await this.userManager
                    .AddToRoleAsync(user, roleName);

                if (!result.Succeeded)
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<bool> RemoveUserRoleAsync(Guid userId, string roleName)
        {
            User? user = await userManager
                .FindByIdAsync(userId.ToString());
            bool roleExists = await this.roleManager.RoleExistsAsync(roleName);

            if (user == null || !roleExists)
            {
                return false;
            }

            bool alreadyInRole = await this.userManager.IsInRoleAsync(user, roleName);
            if (alreadyInRole)
            {
                IdentityResult? result = await this.userManager
                    .RemoveFromRoleAsync(user, roleName);

                if (!result.Succeeded)
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            User? user = await userManager
                .FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return false;
            }

            IdentityResult? result = await this.userManager
                .DeleteAsync(user);
            if (!result.Succeeded)
            {
                return false;
            }

            return true;
        }
    }
}
