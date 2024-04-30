using DealDex.Core.Entities;

namespace Tecnm.Ecommerce1.Api.Repositories.Interfecies.Supplier;

public interface ISupplierInfoRepository
{
    Task<SupplierInfo> SaveAsycn(SupplierInfo category);
    
    Task<SupplierInfo> UpdateAsync(SupplierInfo category);
    
    Task<List<SupplierInfo>> GetAllAsync();
    
    Task<bool> DeleteAsync(int id);
    
    Task<SupplierInfo> GetById(int id);
}