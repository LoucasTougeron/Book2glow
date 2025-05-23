using Book2Glow.Infrastructure.Data.Model;
using Book2Glow.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book2Glow.Service.Service
{
    public interface IBusinessService
    {
        Task<List<BusinessModel>> GetAll();
        Task<BusinessModel> GetById(Guid id);
        Task<BusinessModel> Create(BusinessModel business);
        Task<BusinessModel> Update(Guid id, BusinessModel business);
        Task<bool> Delete(Guid id);
        Task<List<BusinessModel>> GetBuisnessByUser(string id);

        Task<List<BusinessModel>> GetBuisnessByCity (string city);

        Task AddCategoryToBusinessAsync(Guid businessId, Guid categoryId);

        Task<List<BusinessModel>> GetBusinessByCategoryAndCity(Guid categoryId, string city);
    }
}
