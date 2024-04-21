using DealDex.Core.Entities;

namespace DealDex.Api.Dto.Supplier;

public class SupplierInfoDto :DtoBase
{
    public string Nombre { get; set; }
    public string Direccion { get; set; }
    public string Cuidad { get; set; }
    public string Correo { get; set; }

    public SupplierInfoDto()
    {
        
    }


    public SupplierInfoDto(SupplierInfo category)
    {
       Nombre  = category.Nombre;
        id = category.id;
        Direccion = category.Direccion;
        Cuidad = category.Cuidad;
        Correo = category.Correo;
    }
}