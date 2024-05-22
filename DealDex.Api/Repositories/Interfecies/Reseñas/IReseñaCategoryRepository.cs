using DealDex.Core.Entities;

namespace Tecnm.Ecommerce1.Api.Repositories.Interfecies.Reseñas;

public interface IReseñaCategoryRepository
{
    Task<ReseñaCategory> SaveAsycn(ReseñaCategory category);
    
    Task<ReseñaCategory> UpdateAsync(ReseñaCategory category);
    
    Task<List<ReseñaCategory>> GetAllAsync();
    
    Task<bool> DeleteAsync(int id);
    
    Task<ReseñaCategory> GetById(int id);
    
    Task<ReseñaCategory> GetByName(string name, int id = 0);

}