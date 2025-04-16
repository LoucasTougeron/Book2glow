using Book2Glow.Infrastructure.Data.Model;
using Book2Glow.Service.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Book2Glow.Api.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController: ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly UserManager<ApplicationUser> _userManager;
        public CategoryController(ICategoryService categoryService, UserManager<ApplicationUser> userManager) 
        {
            _categoryService = categoryService;
            _userManager = userManager;
        }

        // GET: api/category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAll();
            return Ok(categories);
        }

        // GET: api/category/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModel>> GetCategoryById(Guid id)
        {

            var category = await _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound(new { message = "category not found" });
            }
            return Ok(category);
        }

        // POST: api/category
        [HttpPost]
        [ServiceFilter(typeof(RoleMiddleware))]
        [AuthorizeRole("Provider")]
        public async Task<ActionResult<CategoryModel>> CreateCategory([FromBody] CategoryModel newCategory)
        {
            if (newCategory == null)
            {
                return BadRequest(new { message = "Invalid category data" });
            }

            var createdCategory = await _categoryService.Create(newCategory);
            return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.Id }, createdCategory);
        }

        // PUT: api/category/{id}
        [HttpPut("{id}")]
        [ServiceFilter(typeof(RoleMiddleware))]
        [AuthorizeRole("Provider")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryModel updatedCategory)
        {
            try
            {
                var updated = await _categoryService.Update(id, updatedCategory);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Category not found" });
            }
        }
        
        // DELETE: api/category/{id}
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(RoleMiddleware))]
        [AuthorizeRole("Provider")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new { message = "Invalid ID format" });
            }

            var deleted = await _categoryService.Delete(id);
            if (!deleted)
            {
                return NotFound(new { message = "Category not found" });
            }

            return NoContent();
        }
    }
}
