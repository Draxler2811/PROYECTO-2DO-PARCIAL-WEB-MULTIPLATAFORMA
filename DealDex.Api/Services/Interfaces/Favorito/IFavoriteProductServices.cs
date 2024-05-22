using DealDex.Api.Dto.Favoritos;

namespace Tecnm.Ecommerce1.Api.Services.Interfaces.Favorito;

public interface IFavoriteProductServices
{
    Task<bool> FavoriteProductExist(int id);
    
    Task<FavoriteProductDto> SaveAsyc(FavoriteProductDto category);
    
    Task<FavoriteProductDto> UpdateAsync(FavoriteProductDto category);
    
    Task<List<FavoriteProductDto>> GetAllAsync();
    
    Task<bool> DeleteAsync(int id);
    
    Task<FavoriteProductDto> GetById(int id);
    Task<bool> ExistByName(string name, int id = 0);

}