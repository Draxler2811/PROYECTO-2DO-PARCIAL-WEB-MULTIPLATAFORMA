using DealDex.Api.Dto.Reseñas;

namespace Tecnm.Ecommerce1.Api.Services.Interfaces.Reseñas;

public interface IReseñasCategoryServices
{
    Task<bool> ReseñaCategoryExist(int id);

    Task<ReseñaCategoryDto> SaveAsycn(ReseñaCategoryDto category);
    
    Task<ReseñaCategoryDto> UpdateAsync(ReseñaCategoryDto category);
    
    Task<List<ReseñaCategoryDto>> GetAllAsync();
    
    Task<bool> DeleteAsync(int id);
    
    Task<ReseñaCategoryDto> GetById(int id);
    Task<bool> ExistByName(string name, int id = 0);

}