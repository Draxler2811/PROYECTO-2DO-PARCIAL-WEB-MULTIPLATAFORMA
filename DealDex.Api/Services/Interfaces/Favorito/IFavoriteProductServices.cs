using DealDex.Api.Dto.Favoritos;

namespace Tecnm.Ecommerce1.Api.Services.Interfaces.Favorito;

public interface IFavoriteProductServices
{
    Task<bool> FavoriteProductExist(int id);
    
    //Metodo para guardar las categorias de producto
    Task<FavoriteProductDto> SaveAsyc(FavoriteProductDto category);
    
    //Metodo para Actualizar las categorias de producto
    Task<FavoriteProductDto> UpdateAsync(FavoriteProductDto category);
    
    //Metodo para retornar una lista de categorias de productos
    Task<List<FavoriteProductDto>> GetAllAsync();
    
    //Metodo para retornar el id de las categorias del producto que se borrarar
    Task<bool> DeleteAsync(int id);
    
    //Metodo para obtener una categoria por id
    Task<FavoriteProductDto> GetById(int id);
}