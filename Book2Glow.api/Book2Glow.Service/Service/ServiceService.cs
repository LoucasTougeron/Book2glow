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
    public class ServiceService : IServiceService
    {
        private readonly DataModelContext _context;

        public ServiceService(DataModelContext context)
        {
            _context = context;
        }
        public async Task<ServiceModel> Create(ServiceModel service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
            return service;
        }

        public async Task<bool> Delete(Guid id)
        {
            var service = await _context.Services.FirstOrDefaultAsync(b => b.Id == id);
            if (service == null)
            {
                return false; // Service non trouvé
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<ServiceModel>> GetAll()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<ServiceModel> GetById(Guid id)
        {
            return await _context.Services.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<ServiceDto>> GetServicesByCategoryAndCity(Guid categoryId, string city)
        {
            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == categoryId);
            if (!categoryExists)
            {
                throw new KeyNotFoundException("Category not found");
            }

            var services = await _context.Services
                .Include(s => s.BusinessCategory)
                .ThenInclude(bc => bc.Business)
                    .Where(s =>
                        s.BusinessCategory.CategoryId == categoryId &&
                        s.BusinessCategory.Business.City.ToLower() == city.ToLower())
                    .Select(s => new ServiceDto
                    {
                        Id = s.Id,
                        Name = s.name,
                        Amount = s.amount,
                        Duration = s.duration,
                        BusinessName = s.BusinessCategory.Business.Name,
                        City = s.BusinessCategory.Business.City
                    })
                    .ToListAsync();

            return services;
        }

        public async Task<ServiceModel> Update(Guid id, ServiceModel service)
        {
            var existingService = await _context.Services.FirstOrDefaultAsync(b => b.Id == id);
            if (existingService == null)
            {
                throw new KeyNotFoundException("Business not found");
            }

            _context.Entry(existingService).CurrentValues.SetValues(service);
            await _context.SaveChangesAsync();

            return existingService;
        }
    }
}
