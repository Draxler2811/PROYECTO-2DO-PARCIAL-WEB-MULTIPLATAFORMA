using DealDex.Core.Entities;

namespace DealDex.Api.Repositories.Interfecies;

public interface IUsersCategoryRepository
{
    //Metodo para guardar las categorias de producto
    Task<UsersCategory> SaveAsycn(UsersCategory category);
    
    //Metodo para Actualizar las categorias de producto
    Task<UsersCategory> UpdateAsync(UsersCategory category);
    
    //Metodo para retornar una lista de categorias de productos
    Task<List<UsersCategory>> GetAllAsync();
    
    //Metodo para retornar el id de las categorias del producto que se borrarar
    Task<bool> DeleteAsync(int id);
    
    //Metodo para obtener una categoria por id
    Task<UsersCategory> GetById(int id);
    
    Task<UsersCategory> GetUserByEmailAndPassword(string correo, string contraseña);

}