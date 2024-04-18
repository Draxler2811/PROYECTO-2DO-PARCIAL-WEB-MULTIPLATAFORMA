using DealDex.Api.Dto.Carrito;

namespace Tecnm.Ecommerce1.Api.Services.Interfaces.Carrito;

public interface ICarritoCategoryServices
{
    Task<bool> CarritoCategoryExist(int id);
    
    //Metodo para guardar las categorias de producto
    Task<CarritoCategoryDto> SaveAsyc(CarritoCategoryDto category);
    
    //Metodo para Actualizar las categorias de producto
    Task<CarritoCategoryDto> UpdateAsync(CarritoCategoryDto category);
    
    //Metodo para retornar una lista de categorias de productos
    Task<List<CarritoCategoryDto>> GetAllAsync();
    
    //Metodo para retornar el id de las categorias del producto que se borrarar
    Task<bool> DeleteAsync(int id);
    
    //Metodo para obtener una categoria por id
    Task<CarritoCategoryDto> GetById(int id);
}