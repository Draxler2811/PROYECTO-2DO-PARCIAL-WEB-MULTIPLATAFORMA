using DealDex.Api.Dto.Categories;
using DealDex.Core.Http;

namespace DealDex.WebSite.Services.Interfaces;

public interface ICategoryService
{
    Task<Response<List<CategoryTypeDto>>> GetAllAsync();
    
    Task<Response<CategoryTypeDto>> GetById(int id);
    
    Task<Response<CategoryTypeDto>> SaveAsync(CategoryTypeDto categoryDto);
    
    Task<Response<CategoryTypeDto>> UpdateAsync(CategoryTypeDto categoryDto);

    Task<Response<bool>> DeleteAsync(int id);

}