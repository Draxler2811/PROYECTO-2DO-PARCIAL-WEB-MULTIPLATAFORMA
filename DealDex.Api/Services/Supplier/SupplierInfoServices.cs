using DealDex.Api.Dto.Supplier;
using DealDex.Core.Entities;
using Tecnm.Ecommerce1.Api.Repositories.Interfecies.Supplier;
using Tecnm.Ecommerce1.Api.Services.Interfaces.Supplier;

namespace Tecnm.Ecommerce1.Api.Services.Supplier;

public class SupplierInfoServices : ISupplierInfoService
{
    private readonly ISupplierInfoRepository _supplierInfoRepository;
    

    public SupplierInfoServices(ISupplierInfoRepository supplierInfoRepository)
    {
        _supplierInfoRepository = supplierInfoRepository;
    }
    
    
    
    public async Task<bool> SupplierInfoExist(int id)
    {
        var category = await _supplierInfoRepository.GetById(id);
        return (category != null);
    }
    public async Task<SupplierInfoDto> SaveAsycn(SupplierInfoDto supplierInfoDto)
    {
        var supplier = new SupplierInfo
        {
            
            Nombre = supplierInfoDto.Nombre,
            Direccion = supplierInfoDto.Direccion,
            Cuidad = supplierInfoDto.Cuidad,
            Correo = supplierInfoDto.Correo,
            CreatedBy = "Omar",
            CreatedDate = DateTime.Now,
            UpdatedBy = "Omar",
            UpdatedDate = DateTime.Now
        };
        
        supplier = await _supplierInfoRepository.SaveAsycn(supplier);
        supplierInfoDto.id = supplier.id;
        return supplierInfoDto;
    }

    public async Task<SupplierInfoDto> UpdateAsync(SupplierInfoDto supplierInfoDto)
    {
        var supplier = await _supplierInfoRepository.GetById(supplierInfoDto.id);

        if (supplier == null)
            throw new Exception("Product Category Not founf");
        supplier.Nombre = supplierInfoDto.Nombre;
        supplier.Direccion = supplierInfoDto.Direccion;
        supplier.Cuidad = supplierInfoDto.Cuidad;
        supplier.Correo = supplierInfoDto.Correo;
      
        
        supplier.UpdatedBy = "Omar";
        supplier.UpdatedDate = DateTime.Now;
        await _supplierInfoRepository.UpdateAsync(supplier);
        return supplierInfoDto;
    }

    public async Task<List<SupplierInfoDto>> GetAllAsync()
    {
        var categories = await _supplierInfoRepository.GetAllAsync();
        var categoriesDto = categories.Select(c => new SupplierInfoDto(c)).ToList();
        return categoriesDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _supplierInfoRepository.DeleteAsync(id);
    }

    public async Task<SupplierInfoDto> GetById(int id)
    {
        var category = await _supplierInfoRepository.GetById(id);
        if (category == null)
            throw new Exception("Product category not Found");
        var categoryDto = new SupplierInfoDto()
        {
            Nombre = category.Nombre,
            Direccion = category.Direccion,
            Cuidad = category.Cuidad,
            Correo = category.Correo
        };
        return categoryDto;
    }
}