using DealDex.Api.Dto;
using DealDex.Api.Dto.Carrito;
using DealDex.Core.Http;

namespace DealDex.WebSite.Services.Interfaces;

public interface ICartService
{
    Task<Response<List<CarritoCategoryDto>>> GetAllAsync();
    
    Task<Response<CarritoCategoryDto>> GetById(int id);
    
    Task<Response<CarritoCategoryDto>> SaveAsync(CarritoCategoryDto carritoDto);
    
    Task<Response<CarritoCategoryDto>> UpdateAsync(CarritoCategoryDto carritoDto);

    Task<Response<bool>> DeleteAsync(int id);
}