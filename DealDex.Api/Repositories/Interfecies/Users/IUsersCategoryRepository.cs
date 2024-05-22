using DealDex.Core.Entities;

namespace DealDex.Api.Repositories.Interfecies;

public interface IUsersCategoryRepository
{
    Task<UsersCategory> SaveAsycn(UsersCategory category);
    
    Task<UsersCategory> UpdateAsync(UsersCategory category);
    
    Task<List<UsersCategory>> GetAllAsync();
    
    Task<bool> DeleteAsync(int id);
    
    Task<UsersCategory> GetById(int id);
    
    Task<UsersCategory> GetUserByEmailAndPassword(string correo, string contraseña);
    
    Task<UsersCategory> GetByName(string name, int id = 0);


}