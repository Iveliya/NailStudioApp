using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace NailStudioApp.Web.ViewModels.NailStudio
{
    using static Common.EntityValidationConstans.Service;
    using static Common.EntityValidationMessages.Service;
    public class AddServiceInputModel
    {
        [Required(ErrorMessage = NameRequiredMessage)]
        [MaxLength(NameMaxLength)]
        public static string Name { get; set; } = null!;
        [Required(ErrorMessage =DescriptionRequiredMessage)]
        [MaxLength(DescriptionMaxLength)]
        public static string Description { get; set; }

    }
}
