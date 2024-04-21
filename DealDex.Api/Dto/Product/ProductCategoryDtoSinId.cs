﻿using DealDex.Api.Dto.Categories;
using DealDex.Core.Entities;

namespace DealDex.Api.Dto;

public class ProductCategoryDtoSinId
{
    public string Image { get; set; }
    public string Titulo { get; set; }
    public decimal Precio { get; set; }
    public string Descripcion { get; set; }

    public int IdCategory { get; set; }

    public string Estado { get; set; }
    public string Ubicacion { get; set; }

    public int IdSupplier { get; set; }



    public ProductCategoryDtoSinId()
    {
        
    }

    public ProductCategoryDtoSinId(ProductCategory category)
    {
        IdSupplier = category.IdSupplier;
        Image = category.Image;
        Titulo = category.Titulo;
        Precio = category.Precio;
        Descripcion = category.Descripcion;
        IdCategory = category.IdCategory;
        Estado = category.Estado;
        Ubicacion = category.Ubicacion;
       
    }
}