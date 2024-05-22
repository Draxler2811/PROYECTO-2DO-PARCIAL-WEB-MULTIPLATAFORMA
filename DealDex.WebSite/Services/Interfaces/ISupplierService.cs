using DealDex.Api.Dto.Supplier;
using DealDex.Core.Http;

namespace DealDex.WebSite.Services.Interfaces;

public interface ISupplierService
{
     Task<Response<List<SupplierInfoDto>>> GetAllAsync();
        
        Task<Response<SupplierInfoDto>> GetById(int id);
        
        Task<Response<SupplierInfoDto>> SaveAsync(SupplierInfoDto supplierDto);
        
        Task<Response<SupplierInfoDto>> UpdateAsync(SupplierInfoDto supplierDto);
    
        Task<Response<bool>> DeleteAsync(int id);

}