using NailStudioApp.Web.ViewModel.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailStudioApp.Services.Data.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<IndexReviewViewModel>> GetAllReviewsAsync();
        Task AddReviewAsync(AddReviewViewModel model);
    }
}
