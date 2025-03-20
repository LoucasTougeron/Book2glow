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

        public BusinessService(DataModelContext context)
        {
            _context = context;
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

            _context.Entry(existingBusiness).CurrentValues.SetValues(business);
            await _context.SaveChangesAsync();

            return existingBusiness; 
        }
    }
}
