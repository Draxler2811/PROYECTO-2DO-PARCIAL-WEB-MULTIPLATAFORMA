using DealDex.Api.Dto;
using DealDex.Core.Entities;

namespace Tecnm.Ecommerce1.Api.Services.Interfaces.Users;

public interface IUsersCategoryService
{
    Task<bool> UsersCategoryExist(int id);
    
    //Metodo para guardar las categorias de producto
    Task<UsersCategoryDto> SaveAsycn(UsersCategoryDto category);
    
    //Metodo para Actualizar las categorias de producto
    Task<UsersCategoryDto> UpdateAsync(UsersCategoryDto category);
    
    //Metodo para retornar una lista de categorias de productos
    Task<List<UsersCategoryDto>> GetAllAsync();
    
    //Metodo para retornar el id de las categorias del producto que se borrarar
    Task<bool> DeleteAsync(int id);
    
    //Metodo para obtener una categoria por id
    Task<UsersCategoryDto> GetById(int id);
}