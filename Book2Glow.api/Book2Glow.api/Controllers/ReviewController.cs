using Book2Glow.Api.Dtos;
using Book2Glow.Service.Dto;
using Book2Glow.Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Book2Glow.Api.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IUserService _userService;

        public ReviewController(IReviewService reviewService, IUserService userService)
        {
            _reviewService = reviewService;
            _userService = userService;
        }

        [HttpPost]
        [ServiceFilter(typeof(RoleMiddleware))]
        public async Task<IActionResult> AddReview([FromBody] ReviewDto dto)
        {
            var user = await _userService.GetCurrentUserAsync();
            try
            {
                var review = await _reviewService.AddReviewAsync(dto, user.Id);
                return Ok(review);
            }
            catch (UnauthorizedAccessException e)
            {
                return Forbid(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("businesses/{businessId}")]
        public async Task<IActionResult> GetBusinessReviews(Guid businessId)
        {
            var reviews = await _reviewService.GetReviewsForBusinessAsync(businessId);
            return Ok(reviews);
        }

        [HttpGet("services/{serviceId}")]
        public async Task<IActionResult> GetServiceReviews(Guid serviceId)
        {
            var reviews = await _reviewService.GetReviewsForServiceAsync(serviceId);
            return Ok(reviews);
        }
    }
}
