using NailStudio.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;


namespace NailStudioApp.Web.ViewModel.Admin.UserManagement
{
    using AutoMapper;
    using NailStudioApp.Services.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class AllUsersViewModel
    {
        [Required(ErrorMessage = "User ID is required.")]
        public string Id { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "The email address is not valid.")]
        public string? Email { get; set; }

        [MinLength(1, ErrorMessage = "At least one role must be assigned to the user.")]
        public IEnumerable<string> Roles { get; set; } = new List<string>();
    }
}
