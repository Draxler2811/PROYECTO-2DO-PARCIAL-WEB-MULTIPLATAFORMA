using DealDex.Core.Entities;

namespace Tecnm.Ecommerce1.Api.Repositories.Interfecies.Supplier;

public interface ISupplierInfoRepository
{
    //Metodo para guardar las categorias de producto
    Task<SupplierInfo> SaveAsycn(SupplierInfo category);
    
    //Metodo para Actualizar las categorias de producto
    Task<SupplierInfo> UpdateAsync(SupplierInfo category);
    
    //Metodo para retornar una lista de categorias de productos
    Task<List<SupplierInfo>> GetAllAsync();
    
    //Metodo para retornar el id de las categorias del producto que se borrarar
    Task<bool> DeleteAsync(int id);
    
    //Metodo para obtener una categoria por id
    Task<SupplierInfo> GetById(int id);
}