using DealDex.Core.Entities;

namespace DealDex.Api.Dto.Favoritos;

public class FavoriteProductDtoSinId
{
    public string Image { get; set; }
    public string Titulo { get; set; }
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }
    
    public FavoriteProductDtoSinId()
    {
        
    }

    public FavoriteProductDtoSinId(FavoriteProduct category)
    {
        
        Image = category.Imagen;
        Titulo = category.Titulo;
        Precio = category.Precio;
        Cantidad = category.Cantidad;

    }
}