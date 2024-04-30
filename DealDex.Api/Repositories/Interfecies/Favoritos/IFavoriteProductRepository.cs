using DealDex.Core.Entities;

namespace Tecnm.Ecommerce1.Api.Repositories.Interfecies.Favoritos;

public interface IFavoriteProductRepository
{
    Task<FavoriteProduct> SaveAsycn(FavoriteProduct category);
    
    Task<FavoriteProduct> UpdateAsync(FavoriteProduct category);
    
    Task<List<FavoriteProduct>> GetAllAsync();
    
    Task<bool> DeleteAsync(int id);
    
    Task<FavoriteProduct> GetById(int id);
}