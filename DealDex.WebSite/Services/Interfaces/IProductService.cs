using DealDex.Core.Http;
using DealDex.Api.Dto;
namespace DealDex.WebSite.Services.Interfaces;

public interface IProductService
{
    Task<Response<List<ProductCategoryDtoAdd>>> GetAllAsync();
    
    Task<Response<ProductCategoryDtoAdd>> GetById(int id);
    
    Task<Response<ProductCategoryDtoAdd>> SaveAsync(ProductCategoryDtoAdd productCategoryDto);
    
    Task<Response<ProductCategoryDtoAdd>> UpdateAsync(ProductCategoryDtoAdd productCategoryDto);
    
    Task<Response<bool>> DeleteAsync(int id);
}