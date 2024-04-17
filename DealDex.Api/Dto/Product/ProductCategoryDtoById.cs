using DealDex.Core.Entities;

namespace DealDex.Api.Dto;

public class ProductCategoryDtoById : DtoBase
{
    public string Image { get; set; }
    public string Titulo { get; set; }
    public decimal Precio { get; set; }
    public string Descripcion { get; set; }
    
    public ProductCategoryDtoById()
    {
        
    }

    public ProductCategoryDtoById(ProductCategory category)
    {
        id = category.id;
        Image = category.Image;
        Titulo = category.Titulo;
        Precio = category.Precio;
        Descripcion = category.Descripcion;
    }
}