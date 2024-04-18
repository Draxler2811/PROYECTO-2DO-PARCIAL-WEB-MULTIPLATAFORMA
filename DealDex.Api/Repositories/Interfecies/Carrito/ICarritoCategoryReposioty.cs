using DealDex.Core.Entities;

namespace Tecnm.Ecommerce1.Api.Repositories.Interfecies.Carrito;

public interface ICarritoCategoryReposioty
{
    //Metodo para guardar las categorias de producto
    Task<CarritoCategory> SaveAsycn(CarritoCategory category);
    
    //Metodo para Actualizar las categorias de producto
    Task<CarritoCategory> UpdateAsync(CarritoCategory category);
    
    //Metodo para retornar una lista de categorias de productos
    Task<List<CarritoCategory>> GetAllAsync();
    
    //Metodo para retornar el id de las categorias del producto que se borrarar
    Task<bool> DeleteAsync(int id);
    
    //Metodo para obtener una categoria por id
    Task<CarritoCategory> GetById(int id);
}