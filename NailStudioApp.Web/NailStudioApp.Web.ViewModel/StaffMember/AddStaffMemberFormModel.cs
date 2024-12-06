using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Web.ViewModel.StaffMember
{
    public class AddStaffMemberFormModel
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string PhotoUrl { get; set; }
    }
}
