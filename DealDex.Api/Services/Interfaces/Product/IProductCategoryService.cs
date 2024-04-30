using DealDex.Api.Dto;

namespace DealDex.Api.Services.Interfaces;

public interface IProductCategoryService
{
    Task<bool> ProductCategoryExist(int id);
    
    Task<ProductCategoryDtoAdd> SaveAsycn(ProductCategoryDtoAdd category);
    
    Task<ProductCategoryDtoAdd> UpdateAsync(ProductCategoryDtoAdd category);
    
    Task<List<ProductCategoryDto>> GetAllAsync();
    
    Task<bool> DeleteAsync(int id);
    
    Task<ProductCategoryDtoById> GetById(int id);
}