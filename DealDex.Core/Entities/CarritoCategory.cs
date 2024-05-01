using System.Security.AccessControl;

namespace DealDex.Core.Entities;

public class CarritoCategory : EntityBase{
    public string Imagen { get; set; }
    public string Titulo { get; set; }
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }

    public int IdUser { get; set; }
    public int IdProducto { get; set; }

}