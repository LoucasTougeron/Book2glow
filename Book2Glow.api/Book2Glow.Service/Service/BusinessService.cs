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
    public class BusinessService : IBusinessService
    {
        private readonly DataModelContext _context;
        private readonly IUserService _userService;

        public BusinessService(DataModelContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<BusinessModel> Create(BusinessModel business)
        {
            _context.Businesses.Add(business);
            await _context.SaveChangesAsync();
            return business;
        }

        public async Task<bool> Delete(Guid id)
        {
            var business = await _context.Businesses.FirstOrDefaultAsync(b => b.Id == id);
            if (business == null)
            {
                return false; // Business non trouvé
            }

            _context.Businesses.Remove(business);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<BusinessModel>> GetAll()
        {
            return await _context.Businesses.ToListAsync();
        }

        public async Task<BusinessModel> GetById(Guid id)
        {
            return await _context.Businesses.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<BusinessModel> Update(Guid id, BusinessModel business)
        {

            var existingBusiness = await _context.Businesses.FirstOrDefaultAsync(b => b.Id == id);
            if (existingBusiness == null)
            {
                throw new KeyNotFoundException("Business not found");
            }

            var user = await _userService.GetCurrentUserAsync();
            if (user.Id != existingBusiness.ApplicationUserId)
            {
                throw new Exception("User is not creator of buiness");
            }

            _context.Entry(existingBusiness).CurrentValues.SetValues(business);
            await _context.SaveChangesAsync();

            return existingBusiness; 
        }

        public async Task<List<BusinessModel>> GetBuisnessByUser(string id)
        {
            return await _context.Businesses
            .Where(b => b.ApplicationUserId == id)
            .ToListAsync();
        }

        public async Task<List<BusinessModel>> GetBuisnessByCity(string city)
        {
            return await _context.Businesses
            .Where(b => b.City == city)
            .ToListAsync();
        }

        public async Task AddCategoryToBusinessAsync(Guid businessId, Guid categoryId)
        {
            var existingBusiness = await _context.Businesses
                .FirstOrDefaultAsync(b => b.Id == businessId);

            if (existingBusiness == null)
            {
                throw new KeyNotFoundException("Business not found");
            }

            var currentUser = await _userService.GetCurrentUserAsync();
            if (currentUser == null)
            {
                throw new UnauthorizedAccessException("User not authenticated");
            }


            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == categoryId);
            if (category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }

            var existingLink = await _context.BusinessCategories
                .FirstOrDefaultAsync(bc => bc.BusinessId == businessId && bc.CategoryId == categoryId);

            if (existingLink != null)
            {
                throw new InvalidOperationException("Business already linked to this category");
            }

            var businessCategory = new BusinessCategoryModel
            {
                Id = Guid.NewGuid(), 
                BusinessId = businessId,
                CategoryId = categoryId
            };

            _context.BusinessCategories.Add(businessCategory);
            await _context.SaveChangesAsync();
        }

        public async Task<List<BusinessModel>> GetBusinessByCategoryAndCity(Guid categoryId, string city)
        {
            // Vérifie que la catégorie existe
            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == categoryId);
            if (!categoryExists)
            {
                throw new KeyNotFoundException("Category not found");
            }

            var businesses = await _context.BusinessCategories
                .Include(bc => bc.Business)
                .Where(bc =>
                    bc.CategoryId == categoryId &&
                    bc.Business.City.ToLower() == city.ToLower())
                .Select(bc => bc.Business)
                .Distinct()
                .ToListAsync();

            return businesses;
        }

    }
}
