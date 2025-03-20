using Book2Glow.Service.Service;
using Book2Glow.Infrastructure.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Book2Glow.Api.Controllers
{
    [Route("api/buisness")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly IBusinessService _businessService;
        private readonly ILogger<BusinessController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        public BusinessController(IBusinessService businessService, ILogger<BusinessController> logger, UserManager<ApplicationUser> userManager)
        {
            _businessService = businessService;
            _logger = logger;
            _userManager = userManager;
        }


        // GET: api/business
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusinessModel>>> GetAllBusinesses()
        {
            var businesses = await _businessService.GetAll();
            return Ok(businesses);
        }

        // GET: api/buisness/{id}
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

        // POST: api/buisness
        [HttpPost]
        [ServiceFilter(typeof(RoleMiddleware))]
        [AuthorizeRole("Provider")]
        public async Task<ActionResult<BusinessModel>> CreateBusiness([FromBody] BusinessModel newBusiness)
        {
            if (newBusiness == null)
            {
                return BadRequest(new { message = "Invalid business data" });
            }
            var creator = await _userManager.FindByIdAsync(newBusiness.ApplicationUserId);
            newBusiness.Creator = creator;
            var createdBusiness = await _businessService.Create(newBusiness);
            return CreatedAtAction(nameof(GetBusinessById), new { id = createdBusiness.Id }, createdBusiness);
        }

        // PUT: api/buisness/{id}
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

        // DELETE: api/buisness/{id}
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
