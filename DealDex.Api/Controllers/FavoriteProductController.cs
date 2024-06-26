﻿using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using DealDex.Core.Entities;
using DealDex.Core.Http;
using DealDex.Api.Dto;
using DealDex.Api.Dto.Carrito;
using DealDex.Api.Dto.Favoritos;
using DealDex.Api.Repositories.Interfecies;
using DealDex.Api.Services.Interfaces;
using Tecnm.Ecommerce1.Api.Repositories.Interfecies.Carrito;
using Tecnm.Ecommerce1.Api.Services.Interfaces.Favorito;
using Tecnm.Ecommerce1.Api.Services.Interfaces.Users;

namespace DealDex.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class FavoriteProductController : ControllerBase
{
    private readonly IProductCategoryService _productCategoryService;
     private readonly IFavoriteProductServices _favoriteProductServices;
     private readonly IUsersCategoryService _usersCategoryService;

    public FavoriteProductController(IFavoriteProductServices favoriteProductServices,IUsersCategoryService usersCategoryService,
        IProductCategoryService productCategoryService)
    {
        _productCategoryService = productCategoryService;
        _favoriteProductServices = favoriteProductServices;
        _usersCategoryService = usersCategoryService;

    }

    [HttpGet]
    public async Task<ActionResult<Response<List<FavoriteProductDto>>>> GetAll()
    {
        var response = new Response<List<FavoriteProductDto>>
        {
            Data = await _favoriteProductServices.GetAllAsync()
        };

        if (response.Data == null || !response.Data.Any())
        {
            response.Errors.Add("No se encontraron datos.");
            return NotFound(response);
        }

        return Ok(response);
    }

   [HttpPost]
public async Task<ActionResult<Response<FavoriteProductDto>>> Post([FromBody] FavoriteProductDtoSinId favoriteProductDtoSinId)
{
    var response = new Response<FavoriteProductDto>();

    var validationErrors = new List<string>();

    if (await _favoriteProductServices.ExistByName(favoriteProductDtoSinId.Titulo))
    {
        response.Errors.Add($"Titulo {favoriteProductDtoSinId.Titulo} already exists");
        return BadRequest(response);
    }
    
    if (favoriteProductDtoSinId.IdUser == 0)
    {
        validationErrors.Add("El campo IdCategory es obligatorio.");
    }
    if (!await _productCategoryService.ProductCategoryExist(favoriteProductDtoSinId.IdProducto))
    {
        validationErrors.Add("El id del producto  especificado no existe.");
    }
    
    if (favoriteProductDtoSinId.IdProducto == 0)
    {
        validationErrors.Add("El campo IdProducto es obligatorio.");
    }

    if (!await _usersCategoryService.UsersCategoryExist(favoriteProductDtoSinId.IdUser))
    {
        validationErrors.Add("El id del usuario  especificado no existe.");
    }
    
    if (string.IsNullOrEmpty(favoriteProductDtoSinId.Image))
    {
        validationErrors.Add("El campo Image es obligatorio.");
    }

    if (string.IsNullOrEmpty(favoriteProductDtoSinId.Titulo))
    {
        validationErrors.Add("El campo Titulo es obligatorio.");
    }

    if (favoriteProductDtoSinId.Precio <= 0)
    {
        validationErrors.Add("El campo Precio debe ser mayor que cero.");
    }

    if (favoriteProductDtoSinId.Cantidad <= 0)
    {
        validationErrors.Add("El campo Cantidad debe ser mayor que cero.");
    }

    if (validationErrors.Any())
    {
        response.Errors.AddRange(validationErrors);
        return BadRequest(response);
    }

    FavoriteProductDto favoriteDto = new FavoriteProductDto()
    {
        IdProducto = favoriteProductDtoSinId.IdProducto,
        IdUser = favoriteProductDtoSinId.IdUser,
        Image = favoriteProductDtoSinId.Image,
        Titulo = favoriteProductDtoSinId.Titulo,
        Precio = favoriteProductDtoSinId.Precio,
        Cantidad = favoriteProductDtoSinId.Cantidad
    };

    response.Data = await _favoriteProductServices.SaveAsyc(favoriteDto);
    return Created($"/api/[controller]/{response.Data.id}", response);
}


    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<FavoriteProductDto>>> GetById(int id)
    {
        
        var response = new Response<FavoriteProductDto>();
        

        if (!await _favoriteProductServices.FavoriteProductExist(id))
        {
            response.Errors.Add("Id del producto favorito no fue encontrado");
            return NotFound(response);
        }


        response.Data = await _favoriteProductServices.GetById(id); 
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<FavoriteProductDto>>> Update([FromBody] FavoriteProductDto favoritoDto)
    {
        var response = new Response<FavoriteProductDto>();

        var validationErrors = new List<string>();
    
        if (!await _favoriteProductServices.FavoriteProductExist(favoritoDto.id))
        {
            response.Errors.Add("Favorite not found");
            return NotFound(response);
        }
        if (await _favoriteProductServices.ExistByName(favoritoDto.Titulo, favoritoDto.id))
        {
            response.Errors.Add($"Titulo {favoritoDto.Titulo} already exists");
            return BadRequest(response);
        }
        
        if (!await _usersCategoryService.UsersCategoryExist(favoritoDto.IdUser))
        {
            validationErrors.Add("El campo Idusuario especificado no existe.");
        }
    
        if (favoritoDto.IdUser == 0)
        {
            validationErrors.Add("El campo IdCategory es obligatorio.");
        }
        
        if (!await _productCategoryService.ProductCategoryExist(favoritoDto.IdProducto))
        {
            validationErrors.Add("El campo IdProducto especificado no existe.");
        }
        if (!await _usersCategoryService.UsersCategoryExist(favoritoDto.IdUser))
        {
            validationErrors.Add("El id del usuario  especificado no existe.");
        }
        if (favoritoDto.IdProducto == 0)
        {
            validationErrors.Add("El campo IdProducto es obligatorio.");
        }

        if (favoritoDto.id == 0)
        {
            validationErrors.Add("El campo id no puede ser cero.");
        }
    
        if (string.IsNullOrEmpty(favoritoDto.Image))
        {
            validationErrors.Add("El campo Image es obligatorio.");
        }

        if (string.IsNullOrEmpty(favoritoDto.Titulo))
        {
            validationErrors.Add("El campo Titulo es obligatorio.");
        }

        if (favoritoDto.Precio <= 0)
        {
            validationErrors.Add("El campo Precio debe ser mayor que cero.");
        }

        if (favoritoDto.Cantidad <= 0)
        {
            validationErrors.Add("El campo Cantidad debe ser mayor que cero.");
        }

        if (validationErrors.Any())
        {
            response.Errors.AddRange(validationErrors);
            return BadRequest(response);
        }
    
        if (!await _favoriteProductServices.FavoriteProductExist(favoritoDto.id))
        {
            response.Errors.Add("Id del producto favorito no fue encontrado");
            return NotFound(response);
        }

        response.Data = await _favoriteProductServices.UpdateAsync(favoritoDto);
        return Ok(response);
    }

    

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();

        if (!await _favoriteProductServices.DeleteAsync(id))
        {
            response.Errors.Add("id no encontrado");
            return NotFound(response);
        }

        response.Message = "Borrado exitosamente";
        return Ok(response);
    }
}