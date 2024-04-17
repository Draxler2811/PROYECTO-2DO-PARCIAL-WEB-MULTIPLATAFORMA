using DealDex.Core.Entities;

namespace DealDex.Api.Repositories.Interfecies;

public interface IUsersCategoryRepository
{
    //Metodo para guardar las categorias de producto
    Task<UsersCategory> SaveAsycn(UsersCategory category);
    
    //Metodo para Actualizar las categorias de producto
    Task<ProductCategory> UpdateAsync(ProductCategory category);
    
    //Metodo para retornar una lista de categorias de productos
    Task<List<ProductCategory>> GetAllAsync();
    
    //Metodo para retornar el id de las categorias del producto que se borrarar
    Task<bool> DeleteAsync(int id);
    
    //Metodo para obtener una categoria por id
    Task<ProductCategory> GetById(int id);
}