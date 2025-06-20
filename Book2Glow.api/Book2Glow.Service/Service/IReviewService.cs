using Book2Glow.Infrastructure.Data.Model;
using Book2Glow.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book2Glow.Service.Service
{
    public interface IReviewService
    {
        Task<ReviewModel> AddReviewAsync(ReviewDto dto, string userId);
        Task<List<object>> GetReviewsForBusinessAsync(Guid businessId);
        Task<List<object>> GetReviewsForServiceAsync(Guid serviceId);
    }
}
