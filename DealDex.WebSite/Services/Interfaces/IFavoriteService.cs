using DealDex.Api.Dto.Favoritos;
using DealDex.Core.Http;

namespace DealDex.WebSite.Services.Interfaces;

public  interface IFavoriteService
{
    Task<Response<List<FavoriteProductDto>>> GetAllAsync();
    
    Task<Response<FavoriteProductDto>> GetById(int id);
    
    Task<Response<FavoriteProductDto>> SaveAsync(FavoriteProductDto favoriteDto);
    
    Task<Response<FavoriteProductDto>> UpdateAsync(FavoriteProductDto favoriteDto);

    Task<Response<bool>> DeleteAsync(int id);

}