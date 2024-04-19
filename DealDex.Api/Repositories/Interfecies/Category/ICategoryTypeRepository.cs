using DealDex.Core.Entities;

namespace Tecnm.Ecommerce1.Api.Repositories.Interfecies.Category;

public interface ICategoryTypeRepository
{
    //Metodo para guardar las categorias de producto
    Task<CategoryType> SaveAsycn(CategoryType category);
    
    //Metodo para Actualizar las categorias de producto
    Task<CategoryType> UpdateAsync(CategoryType category);
    
    //Metodo para retornar una lista de categorias de productos
    Task<List<CategoryType>> GetAllAsync();
    
    //Metodo para retornar el id de las categorias del producto que se borrarar
    Task<bool> DeleteAsync(int id);
    
    //Metodo para obtener una categoria por id
    Task<CategoryType> GetById(int id);
}