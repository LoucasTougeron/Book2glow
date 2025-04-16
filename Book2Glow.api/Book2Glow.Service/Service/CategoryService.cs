using Book2Glow.Infrastructure.Data;
using Book2Glow.Infrastructure.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book2Glow.Service.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly DataModelContext _context;

        public CategoryService(DataModelContext context)
        {
            _context = context;
        }
        public async Task<CategoryModel> Create(CategoryModel category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> Delete(Guid id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(b => b.Id == id);
            if (category == null)
            {
                return false; // Category non trouvé
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<CategoryModel>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<CategoryModel> GetById(Guid id)
        {
            return await _context.Categories.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<CategoryModel> Update(Guid id, CategoryModel category)
        {
            var existingCategoriy = await _context.Categories.FirstOrDefaultAsync(b => b.Id == id);
            if (existingCategoriy == null)
            {
                throw new KeyNotFoundException("Business not found");
            }

            _context.Entry(existingCategoriy).CurrentValues.SetValues(category);
            await _context.SaveChangesAsync();

            return existingCategoriy;
        }
    }
}
