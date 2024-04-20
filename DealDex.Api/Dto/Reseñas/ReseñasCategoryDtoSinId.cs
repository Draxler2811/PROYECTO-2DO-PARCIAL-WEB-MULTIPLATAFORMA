using DealDex.Core.Entities;

namespace DealDex.Api.Dto.Reseñas;

public class ReseñasCategoryDtoSinId
{
    public string Titulo { get; set; }
    public int Valoracion { get; set; }

    public ReseñasCategoryDtoSinId()
    {
        
    }


    public ReseñasCategoryDtoSinId(ReseñaCategory category)
    {
        Titulo = category.Titulo;
        Valoracion = category.Valoracion;
    }
}