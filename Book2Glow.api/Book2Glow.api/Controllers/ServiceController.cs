using Book2Glow.Infrastructure.Data.Model;
using Book2Glow.Service.Dto;
using Book2Glow.Service.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Book2Glow.Api.Controllers
{
    [Route("api/service")]
    [ApiController]
    public class ServiceController:ControllerBase
    {
        private readonly IServiceService _serviceService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ServiceController(IServiceService serviceService, UserManager<ApplicationUser> userManager)
        {
            _serviceService = serviceService;
            _userManager = userManager;
        }

        // GET: api/service
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceModel>>> GetAllServices()
        {
            var services = await _serviceService.GetAll();
            return Ok(services);
        }

        // GET: api/service/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceModel>> GetServiceById(Guid id)
        {

            var service = await _serviceService.GetById(id);
            if (service == null)
            {
                return NotFound(new { message = "category not found" });
            }
            return Ok(service);
        }

        [HttpGet("by-category-city")]
        public async Task<ActionResult<List<ServiceDto>>> GetServicesByCategoryAndCity([FromQuery] Guid categoryId, [FromQuery] string city)
        {
            try
            {
                var services = await _serviceService.GetServicesByCategoryAndCity(categoryId, city);
                return Ok(services);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/service
        [HttpPost]
        [ServiceFilter(typeof(RoleMiddleware))]
        [AuthorizeRole("Provider")]
        public async Task<ActionResult<CategoryModel>> CreateService([FromBody] ServiceModel newService)
        {
            if (newService == null)
            {
                return BadRequest(new { message = "Invalid category data" });
            }

            var createdCategory = await _serviceService.Create(newService);
            return CreatedAtAction(nameof(GetServiceById), new { id = createdCategory.Id }, createdCategory);

        }

        // PUT: api/service/{id}
        [HttpPut("{id}")]
        [ServiceFilter(typeof(RoleMiddleware))]
        [AuthorizeRole("Provider")]
        public async Task<IActionResult> UpdateService(Guid id, [FromBody] ServiceModel updatedService)
        {
            try
            {
                var updated = await _serviceService.Update(id, updatedService);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Service not found" });
            }
        }

        // DELETE: api/service/{id}
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(RoleMiddleware))]
        [AuthorizeRole("Provider")]
        public async Task<IActionResult> DeleteService(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new { message = "Invalid ID format" });
            }

            var deleted = await _serviceService.Delete(id);
            if (!deleted)
            {
                return NotFound(new { message = "Service not found" });
            }

            return NoContent();
        }
    }
}
