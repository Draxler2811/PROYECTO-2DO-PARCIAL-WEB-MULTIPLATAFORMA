using DealDex.Core.Entities;

namespace DealDex.Api.Dto.Reseñas;

public class ReseñasCategoryDtoSinId
{
    public string Titulo { get; set; }
    public int Valoracion { get; set; }
    public int IdProducto { get; set; }

    public ReseñasCategoryDtoSinId()
    {
        
    }


    public ReseñasCategoryDtoSinId(ReseñaCategory category)
    {
        IdProducto = category.IdProducto;
        Titulo = category.Titulo;
        Valoracion = category.Valoracion;
    }
}