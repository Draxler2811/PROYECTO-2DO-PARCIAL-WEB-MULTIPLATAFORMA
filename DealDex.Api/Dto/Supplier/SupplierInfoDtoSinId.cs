using DealDex.Core.Entities;

namespace DealDex.Api.Dto.Supplier;

public class SupplierInfoDtoSinId
{
    public string Nombre { get; set; }
    public string Direccion { get; set; }
    public string Cuidad { get; set; }
    public string Correo { get; set; }

    public SupplierInfoDtoSinId()
    {
        
    }


    public SupplierInfoDtoSinId(SupplierInfo category)
    {
        Nombre  = category.Nombre;
        Direccion = category.Direccion;
        Cuidad = category.Cuidad;
        Correo = category.Correo;
    }
}