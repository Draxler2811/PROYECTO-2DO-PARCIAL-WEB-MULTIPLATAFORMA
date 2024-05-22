using DealDex.Api.Dto;

namespace DealDex.Api.Services.Interfaces;

public interface IProductCategoryService
{
    Task<bool> ProductCategoryExist(int id);
    
    Task<ProductCategoryDtoAdd> SaveAsycn(ProductCategoryDtoAdd category);
    
    Task<ProductCategoryDtoAdd> UpdateAsync(ProductCategoryDtoAdd category);
    
    Task<List<ProductCategoryDtoAdd>> GetAllAsync();
    
    Task<bool> DeleteAsync(int id);
    
    Task<ProductCategoryDtoAdd> GetById(int id);
    Task<bool> ExistByName(string name, int id = 0);

}