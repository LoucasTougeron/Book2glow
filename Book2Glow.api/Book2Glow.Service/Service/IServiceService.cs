using Book2Glow.Infrastructure.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book2Glow.Service.Service
{
    public interface IServiceService
    {
        Task<List<ServiceModel>> GetAll();
        Task<ServiceModel> GetById(Guid id);
        Task<ServiceModel> Create(ServiceModel service);
        Task<ServiceModel> Update(Guid id, ServiceModel service);
        Task<bool> Delete(Guid id);
    }
}
