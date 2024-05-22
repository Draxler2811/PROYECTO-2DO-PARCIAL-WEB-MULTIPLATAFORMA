using DealDex.Api.Dto.Supplier;
using Tecnm.Ecommerce1.Api.Repositories.Supplier;

namespace Tecnm.Ecommerce1.Api.Services.Interfaces.Supplier;

public interface ISupplierInfoService
{
    Task<bool> SupplierInfoExist(int id);
    
    Task<SupplierInfoDto> SaveAsycn(SupplierInfoDto category);
    
    Task<SupplierInfoDto> UpdateAsync(SupplierInfoDto category);
    
    Task<List<SupplierInfoDto>> GetAllAsync();
    
    Task<bool> DeleteAsync(int id);
    
    Task<SupplierInfoDto> GetById(int id);
    Task<bool> ExistByName(string name, int id = 0);

}