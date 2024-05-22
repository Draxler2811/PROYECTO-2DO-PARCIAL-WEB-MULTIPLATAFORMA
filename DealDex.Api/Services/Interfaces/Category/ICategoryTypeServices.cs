using DealDex.Api.Dto.Categories;

namespace Tecnm.Ecommerce1.Api.Services.Interfaces.category;

public interface ICategoryTypeServices
{
    Task<bool> CategoryTypeExist(int id);
    
    Task<CategoryTypeDto> SaveAsyc(CategoryTypeDto category);
    
    Task<CategoryTypeDto> UpdateAsync(CategoryTypeDto category);
    
    Task<List<CategoryTypeDto>> GetAllAsync();
    
    Task<bool> DeleteAsync(int id);
    
    Task<CategoryTypeDto> GetById(int id);
    Task<bool> ExistByName(string name, int id = 0);

}