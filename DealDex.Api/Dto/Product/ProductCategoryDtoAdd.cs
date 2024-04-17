using DealDex.Core.Entities;

namespace DealDex.Api.Dto;

public class ProductCategoryDtoAdd : DtoBase
{
    public string Image { get; set; }
    public string Titulo { get; set; }
    public decimal Precio { get; set; }
    public string Descripcion { get; set; }

    public string Categoria { get; set; }
    public string Estado { get; set; }
    public string Ubicacion { get; set; }
    
    
    public ProductCategoryDtoAdd()
    {
        
    }

    public ProductCategoryDtoAdd(ProductCategory category)
    {
        id = category.id;
        Image = category.Image;
        Titulo = category.Titulo;
        Precio = category.Precio;
        Descripcion = category.Descripcion;
        Categoria = category.Categoria;
        Estado = category.Estado;
        Ubicacion = category.Ubicacion;

    }
}