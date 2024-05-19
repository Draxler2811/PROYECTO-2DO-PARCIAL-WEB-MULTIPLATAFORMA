using DealDex.Core.Http;
using DealDex.Api.Dto;
namespace DealDex.WebSite.Services.Interfaces;

public interface IProductService
{
    Task<Response<List<ProductCategoryDto>>> GetAllAsync();
    
    Task<Response<ProductCategoryDto>> GetById(int id);
    
    Task<Response<ProductCategoryDto>> SaveAsync(ProductCategoryDto productCategoryDto);
    
    Task<Response<ProductCategoryDto>> UpdateAsync(ProductCategoryDto productCategoryDto);

    Task<Response<bool>> DeleteAsync(int id);
}