using DealDex.Core.Entities;

namespace Tecnm.Ecommerce1.Api.Repositories.Interfecies.Category;

public interface ICategoryTypeRepository
{
    Task<CategoryType> SaveAsycn(CategoryType category);
    
    Task<CategoryType> UpdateAsync(CategoryType category);
    
    Task<List<CategoryType>> GetAllAsync();
    
    Task<bool> DeleteAsync(int id);
    
    Task<CategoryType> GetById(int id);
}