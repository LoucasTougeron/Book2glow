using Book2Glow.Infrastructure.Data.Model;
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
    }
}
