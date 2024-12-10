using NailStudioApp.Web.ViewModel.Admin.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Services.Data.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<AllUsersViewModel>> GetAllUsersAsync();

        Task<bool> UserExistsByIdAsync(Guid userId);

        Task<bool> AssignUserToRoleAsync(Guid userId, string roleName);

        Task<bool> RemoveUserRoleAsync(Guid userId, string roleName);

        Task<bool> DeleteUserAsync(Guid userId);
    }
}
