using Book2Glow.Infrastructure.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book2Glow.Service.Service
{
    public interface ICategoryService
    {
        Task<List<CategoryModel>> GetAll();
        Task<CategoryModel> GetById(Guid id);
        Task<CategoryModel> Create(CategoryModel category);
        Task<CategoryModel> Update(Guid id, CategoryModel category);
        Task<bool> Delete(Guid id);
    }
}
