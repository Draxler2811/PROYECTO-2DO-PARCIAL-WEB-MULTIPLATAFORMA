using DealDex.Api.Dto.Reseñas;

namespace Tecnm.Ecommerce1.Api.Services.Interfaces.Reseñas;

public interface IReseñasCategoryServices
{
    Task<bool> ReseñaCategoryExist(int id);

    //Metodo para guardar las categorias de producto
    Task<ReseñaCategoryDto> SaveAsycn(ReseñaCategoryDto category);
    
    //Metodo para Actualizar las categorias de producto
    Task<ReseñaCategoryDto> UpdateAsync(ReseñaCategoryDto category);
    
    //Metodo para retornar una lista de categorias de productos
    Task<List<ReseñaCategoryDto>> GetAllAsync();
    
    //Metodo para retornar el id de las categorias del producto que se borrarar
    Task<bool> DeleteAsync(int id);
    
    //Metodo para obtener una categoria por id
    Task<ReseñaCategoryDto> GetById(int id);
}