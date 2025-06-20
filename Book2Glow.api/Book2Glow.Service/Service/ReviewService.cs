using Book2Glow.Infrastructure.Data;
using Book2Glow.Infrastructure.Data.Model;
using Book2Glow.Service.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book2Glow.Service.Service
{
    public class ReviewService : IReviewService
    {
        private readonly DataModelContext _context;

        public ReviewService(DataModelContext context)
        {
            _context = context;
        }

        public async Task<ReviewModel> AddReviewAsync(ReviewDto dto, string userId)
        {
            var book = await _context.BookingsBooks
                .Include(b => b.Reviews)
                .FirstOrDefaultAsync(b => b.Id == dto.BookId && b.ApplicationUserId == userId);
            Console.WriteLine(book);

            if (book == null)
                throw new UnauthorizedAccessException("Vous ne pouvez pas laisser un avis sur cette réservation.");

            if (book.Reviews.Any())
                throw new InvalidOperationException("Un avis a déjà été laissé pour cette réservation.");

            var review = new ReviewModel
            {
                Id = Guid.NewGuid(),
                BookId = dto.BookId,
                stars = dto.Stars,
                comments = dto.Comments,
                DateTime = dto.DateTime
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return review;
        }

        public async Task<List<object>> GetReviewsForBusinessAsync(Guid businessId)
        {
            return await _context.Reviews
                .Include(r => r.Book)
                    .ThenInclude(b => b.User)
                .Where(r => r.Book.BusinessId == businessId)
                .Select(r => new
                {
                    r.Id,
                    r.stars,
                    r.comments,
                    r.DateTime,
                    User = r.Book.User.UserName
                })
                .Cast<object>()
                .ToListAsync();
        }

        public async Task<List<object>> GetReviewsForServiceAsync(Guid serviceId)
        {
            return await _context.Reviews
                .Include(r => r.Book)
                    .ThenInclude(b => b.Booking)
                .Include(r => r.Book)
                    .ThenInclude(b => b.User)
                .Where(r => r.Book.Booking.ServiceId == serviceId)
                .Select(r => new
                {
                    r.Id,
                    r.stars,
                    r.comments,
                    r.DateTime,
                    User = r.Book.User.UserName
                })
                .Cast<object>()
                .ToListAsync();
        }
    }

}
