using DealDex.Api.Dto;

namespace DealDex.Api.Services.Interfaces;

public interface IProductCategoryService
{
    Task<bool> ProductCategoryExist(int id);
    
    //Metodo para guardar las categorias de producto
    Task<ProductCategoryDtoAdd> SaveAsycn(ProductCategoryDtoAdd category);
    
    //Metodo para Actualizar las categorias de producto
    Task<ProductCategoryDtoAdd> UpdateAsync(ProductCategoryDtoAdd category);
    
    //Metodo para retornar una lista de categorias de productos
    Task<List<ProductCategoryDto>> GetAllAsync();
    
    //Metodo para retornar el id de las categorias del producto que se borrarar
    Task<bool> DeleteAsync(int id);
    
    //Metodo para obtener una categoria por id
    Task<ProductCategoryDtoById> GetById(int id);
}