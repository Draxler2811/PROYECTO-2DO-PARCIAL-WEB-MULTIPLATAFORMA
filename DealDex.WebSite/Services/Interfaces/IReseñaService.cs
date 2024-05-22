using DealDex.Api.Dto.Reseñas;
using DealDex.Core.Http;

namespace DealDex.WebSite.Services.Interfaces;

public interface IReseñaService
{
    Task<Response<List<ReseñaCategoryDto>>> GetAllAsync();
    
    Task<Response<ReseñaCategoryDto>> GetById(int id);
    
    Task<Response<ReseñaCategoryDto>> SaveAsync(ReseñaCategoryDto reseñaDto);
    
    Task<Response<ReseñaCategoryDto>> UpdateAsync(ReseñaCategoryDto reseñaDto);

    Task<Response<bool>> DeleteAsync(int id);

}