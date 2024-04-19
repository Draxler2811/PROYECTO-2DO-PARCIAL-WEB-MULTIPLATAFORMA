using DealDex.Api.Dto.Categories;

namespace Tecnm.Ecommerce1.Api.Services.Interfaces.category;

public interface ICategoryTypeServices
{
    Task<bool> CategoryTypeExist(int id);
    
    //Metodo para guardar las categorias de producto
    Task<CategoryTypeDto> SaveAsyc(CategoryTypeDto category);
    
    //Metodo para Actualizar las categorias de producto
    Task<CategoryTypeDto> UpdateAsync(CategoryTypeDto category);
    
    //Metodo para retornar una lista de categorias de productos
    Task<List<CategoryTypeDto>> GetAllAsync();
    
    //Metodo para retornar el id de las categorias del producto que se borrarar
    Task<bool> DeleteAsync(int id);
    
    //Metodo para obtener una categoria por id
    Task<CategoryTypeDto> GetById(int id);
}