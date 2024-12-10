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

    public class AllUsersViewModel
    {
        public string Id { get; set; } = null!;

        public string? Email { get; set; }

        public IEnumerable<string> Roles { get; set; } = null!;
    }
}
