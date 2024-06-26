﻿using DealDex.Core.Entities;

namespace DealDex.Api.Dto.Carrito;

public class CarritoCategoryDto : DtoBase
{
    public string Image { get; set; }
    public string Titulo { get; set; }
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }
    public int IdUser { get; set; }
    public int IdProducto{ get; set; }
    
    public CarritoCategoryDto()
    {
        
    }

    public CarritoCategoryDto(CarritoCategory category)
    {
        
        IdUser = category.IdUser;
        IdProducto = category.IdProducto;
        id = category.id;
        Image = category.Imagen;
        Titulo = category.Titulo;
        Precio = category.Precio;
        Cantidad = category.Cantidad;

    }
}