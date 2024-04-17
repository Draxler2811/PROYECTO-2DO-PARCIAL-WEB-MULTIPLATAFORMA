using DealDex.Core.Entities;

namespace DealDex.Api.Dto;

public class ProductCategoryDto : DtoBase
{
    public string Image { get; set; }
    public string Titulo { get; set; }
    public decimal Precio { get; set; }
    


    public ProductCategoryDto()
    {
        
    }

    public ProductCategoryDto(ProductCategory category)
    {
        id = category.id;
        Image = category.Image;
        Titulo = category.Titulo;
        Precio = category.Precio;
       
    }
    
    
}