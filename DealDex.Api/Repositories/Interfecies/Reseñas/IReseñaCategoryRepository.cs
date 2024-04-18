using DealDex.Core.Entities;

namespace Tecnm.Ecommerce1.Api.Repositories.Interfecies.Reseñas;

public interface IReseñaCategoryRepository
{
    //Metodo para guardar las categorias de producto
    Task<ReseñaCategory> SaveAsycn(ReseñaCategory category);
    
    //Metodo para Actualizar las categorias de producto
    Task<ReseñaCategory> UpdateAsync(ReseñaCategory category);
    
    //Metodo para retornar una lista de categorias de productos
    Task<List<ReseñaCategory>> GetAllAsync();
    
    //Metodo para retornar el id de las categorias del producto que se borrarar
    Task<bool> DeleteAsync(int id);
    
    //Metodo para obtener una categoria por id
    Task<ReseñaCategory> GetById(int id);
}