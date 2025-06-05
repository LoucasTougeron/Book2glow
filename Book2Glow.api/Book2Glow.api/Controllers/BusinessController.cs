using Book2Glow.Service.Service;
using Book2Glow.Infrastructure.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Book2Glow.Api.Controllers
{
    [Route("api/business")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly IBusinessService _businessService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        public BusinessController(IBusinessService businessService, ILogger<BusinessController> logger, UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _businessService = businessService;
            _userManager = userManager;
            _userService = userService;
        }


        // GET: api/business
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusinessModel>>> GetAllBusinesses()
        {
            var businesses = await _businessService.GetAll();
            return Ok(businesses);
        }

        // GET: api/business/city/{city}
        [HttpGet("city/{city}")]
        public async Task<ActionResult<BusinessModel>> GetBusinessByCity(string city)
        {

            var business = await _businessService.GetBuisnessByCity(city);
            if (business == null)
            {
                return NotFound(new { message = "Business not found for this city" });
            }
            return Ok(business);
        }
        // GET: api/business/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BusinessModel>> GetBusinessById(Guid id)
        {

            var business = await _businessService.GetById(id);
            if (business == null)
            {
                return NotFound(new { message = "Business not found" });
            }
            return Ok(business);
        }

        // GET /api/buisness/categorycity
        [HttpGet("categorycity")]
        public async Task<ActionResult<List<BusinessModel>>> GetBusinessesByCategoryAndCity([FromQuery] Guid categoryId, [FromQuery] string city)
        {
            try
            {
                var businesses = await _businessService.GetBusinessByCategoryAndCity(categoryId, city);

                if (businesses == null || businesses.Count == 0)
                {
                    return NotFound(new { message = "No businesses found for the specified category and city." });
                }

                return Ok(businesses);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // GET /api/business/myBusiness
        [HttpGet("myBusiness")]
        [ServiceFilter(typeof(RoleMiddleware))]
        [AuthorizeRole("Provider")]
        public async Task<ActionResult<BusinessModel>> GetBusinessByUser()
        {
            var user = await _userService.GetCurrentUserAsync();
            if (user == null)
            {
                return Unauthorized(new { message = "User not authenticated" });
            }
            var businesses = await _businessService.GetBuisnessByUser(user.Id);
            if (businesses == null)
            {
                return NotFound(new { message = "No businesses found for this user" });
            }
            return Ok(businesses);
        }

        // POST: api/business
        [HttpPost]
        [ServiceFilter(typeof(RoleMiddleware))]
        [AuthorizeRole("Provider")]
        public async Task<ActionResult<BusinessModel>> CreateBusiness([FromBody] BusinessModel newBusiness)
        {
            if (newBusiness == null)
            {
                return BadRequest(new { message = "Invalid business data" });
            }
            var user = await _userService.GetCurrentUserAsync();
            newBusiness.ApplicationUserId = user.Id;
            newBusiness.Creator = user;
            var createdBusiness = await _businessService.Create(newBusiness);
            return CreatedAtAction(nameof(GetBusinessById), new { id = createdBusiness.Id }, createdBusiness);
        }

        //POST: /api/business/
        [HttpPost("{businessId}/categories/{categoryId}")]
        [ServiceFilter(typeof(RoleMiddleware))]
        [AuthorizeRole("Provider")]
        public async Task<IActionResult> AddCategoryToBusiness(Guid businessId, Guid categoryId)
        {
            try
            {
                await _businessService.AddCategoryToBusinessAsync(businessId, categoryId);
                return Ok(new { message = "Category added to business successfully" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }

        // PUT: api/business/{id}
        [HttpPut("{id}")]
        [ServiceFilter(typeof(RoleMiddleware))]
        [AuthorizeRole("Provider")]
        public async Task<IActionResult> UpdateBusiness(Guid id, [FromBody] BusinessModel updatedBusiness)
        {
            try
            {
                var updated = await _businessService.Update(id, updatedBusiness);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Business not found" });
            }
        }

        // DELETE: api/business/{id}
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(RoleMiddleware))]
        [AuthorizeRole("Provider")]
        public async Task<IActionResult> DeleteBusiness(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new { message = "Invalid ID format" });
            }

            var deleted = await _businessService.Delete(id);
            if (!deleted)
            {
                return NotFound(new { message = "Business not found" });
            }

            return NoContent();
        }

    }
}
