using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Web.ViewModel.Review
{
    public class AddReviewViewModel
    {
        public string Content { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; }

        public Guid UserId{ get; set; }
    }
}
