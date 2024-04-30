using DealDex.Core.Entities;

namespace DealDex.Api.Repositories.Interfecies;

public interface IProductCategoryRepository
{
    Task<ProductCategory> SaveAsycn(ProductCategory category);
    
    Task<ProductCategory> UpdateAsync(ProductCategory category);
    
    Task<List<ProductCategory>> GetAllAsync();
    
    Task<bool> DeleteAsync(int id);
    
    Task<ProductCategory> GetById(int id);
}