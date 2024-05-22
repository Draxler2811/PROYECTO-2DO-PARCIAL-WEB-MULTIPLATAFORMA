using DealDex.Api.Dto.Carrito;

namespace Tecnm.Ecommerce1.Api.Services.Interfaces.Carrito;

public interface ICarritoCategoryServices
{
    Task<bool> CarritoCategoryExist(int id);
    
    Task<CarritoCategoryDto> SaveAsyc(CarritoCategoryDto category);
    
    Task<CarritoCategoryDto> UpdateAsync(CarritoCategoryDto category);
    
    Task<List<CarritoCategoryDto>> GetAllAsync();
    
    Task<bool> DeleteAsync(int id);
    
    Task<CarritoCategoryDto> GetById(int id);
    Task<bool> ExistByName(string name, int id = 0);

}