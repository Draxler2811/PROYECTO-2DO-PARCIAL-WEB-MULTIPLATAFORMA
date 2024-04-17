
using DealDex.Core.Entities;

namespace DealDex.Core.Entities;

public class ProductCategory : EntityBase
{
    //va herrerar de Entitybase
    public string Image { get; set; }
    public string Titulo { get; set; }
    public decimal Precio  { get; set; }
    public string Categoria { get; set; }
    public string Estado { get; set; }
    public string Descripcion { get; set; }
    public string Ubicacion { get; set; }
    
}