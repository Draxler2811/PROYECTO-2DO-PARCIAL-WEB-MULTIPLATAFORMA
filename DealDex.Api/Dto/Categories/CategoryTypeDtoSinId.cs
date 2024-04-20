using DealDex.Core.Entities;

namespace DealDex.Api.Dto.Categories;

public class CategoryTypeDtoSinId
{
    public string Nombre { get; set; }
   
    public CategoryTypeDtoSinId()
    {
        
    }

    public CategoryTypeDtoSinId(CategoryType category)
    {
        
        Nombre = category.Nombre;

    }
}