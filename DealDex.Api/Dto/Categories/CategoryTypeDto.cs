using DealDex.Core.Entities;

namespace DealDex.Api.Dto.Categories;

public class CategoryTypeDto : DtoBase
{
    public string Nombre { get; set; }
   
    public CategoryTypeDto()
    {
        
    }

    public CategoryTypeDto(CategoryType category)
    {
        id = category.id;
        Nombre = category.Nombre;

    }
    
}