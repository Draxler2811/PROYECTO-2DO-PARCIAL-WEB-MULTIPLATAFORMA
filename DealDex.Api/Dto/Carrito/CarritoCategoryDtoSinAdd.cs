using DealDex.Core.Entities;

namespace DealDex.Api.Dto.Carrito;

public class CarritoCategoryDtoSinAdd
{
    public string Image { get; set; }
    public string Titulo { get; set; }
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }
    public int IdUser { get; set; }
    
    public CarritoCategoryDtoSinAdd()
    {
        
    }
    public CarritoCategoryDtoSinAdd(CarritoCategory category)
    {
        IdUser = category.IdUser;
        Image = category.Imagen;
        Titulo = category.Titulo;
        Precio = category.Precio;
        Cantidad = category.Cantidad;

    }
}