using DealDex.Core.Entities;

namespace DealDex.Api.Dto.Reseñas;

public class ReseñaCategoryDto : DtoBase
{
    public string Titulo { get; set; }
    public int Valoracion { get; set; }
    public int IdProducto { get; set; }

    public ReseñaCategoryDto()
    {
        
    }


    public ReseñaCategoryDto(ReseñaCategory category)
    {
        IdProducto = category.IdProducto;
        id = category.id;
        Titulo = category.Titulo;
        Valoracion = category.Valoracion;
    }
}