namespace DealDex.Core.Entities;

public class FavoriteProduct : EntityBase
{
    public string Imagen { get; set; }
    public string Titulo { get; set; }
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }

    public int IdUser { get; set; }
    
}