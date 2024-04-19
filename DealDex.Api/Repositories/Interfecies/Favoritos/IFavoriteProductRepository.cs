using DealDex.Core.Entities;

namespace Tecnm.Ecommerce1.Api.Repositories.Interfecies.Favoritos;

public interface IFavoriteProductRepository
{
    //Metodo para guardar las categorias de producto
    Task<FavoriteProduct> SaveAsycn(FavoriteProduct category);
    
    //Metodo para Actualizar las categorias de producto
    Task<FavoriteProduct> UpdateAsync(FavoriteProduct category);
    
    //Metodo para retornar una lista de categorias de productos
    Task<List<FavoriteProduct>> GetAllAsync();
    
    //Metodo para retornar el id de las categorias del producto que se borrarar
    Task<bool> DeleteAsync(int id);
    
    //Metodo para obtener una categoria por id
    Task<FavoriteProduct> GetById(int id);
}