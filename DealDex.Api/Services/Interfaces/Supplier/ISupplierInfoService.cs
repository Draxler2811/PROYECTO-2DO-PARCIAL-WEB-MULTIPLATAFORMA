using DealDex.Api.Dto.Supplier;
using Tecnm.Ecommerce1.Api.Repositories.Supplier;

namespace Tecnm.Ecommerce1.Api.Services.Interfaces.Supplier;

public interface ISupplierInfoService
{
    Task<bool> SupplierInfoExist(int id);
    
    //Metodo para guardar las categorias de producto
    Task<SupplierInfoDto> SaveAsycn(SupplierInfoDto category);
    
    //Metodo para Actualizar las categorias de producto
    Task<SupplierInfoDto> UpdateAsync(SupplierInfoDto category);
    
    //Metodo para retornar una lista de categorias de productos
    Task<List<SupplierInfoDto>> GetAllAsync();
    
    //Metodo para retornar el id de las categorias del producto que se borrarar
    Task<bool> DeleteAsync(int id);
    
    //Metodo para obtener una categoria por id
    Task<SupplierInfoDto> GetById(int id);
}