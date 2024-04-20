using System.Data;
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

namespace DealDex.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class FavoriteProductController : ControllerBase
{
     private readonly IFavoriteProductServices _favoriteProductServices;
    
    public FavoriteProductController(IFavoriteProductServices favoriteProductServices)
    {
        
        _favoriteProductServices = favoriteProductServices;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<FavoriteProductDto>>>> GetAll()
    {
        var response = new Response<List<FavoriteProductDto>>
        {
            Data = await _favoriteProductServices.GetAllAsync()
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<FavoriteProductDto>>> Post([FromBody] FavoriteProductDtoSinId favoriteProductDtoSinId)
    {
        var response = new Response<FavoriteProductDto>();

        var validationErrors = new List<string>();

        // Validación del campo Image
        if (string.IsNullOrEmpty(favoriteProductDtoSinId.Image))
        {
            validationErrors.Add("El campo Image es obligatorio.");
        }

        // Validación del campo Titulo
        if (string.IsNullOrEmpty(favoriteProductDtoSinId.Titulo))
        {
            validationErrors.Add("El campo Titulo es obligatorio.");
        }

        // Validación del campo Precio
        if (favoriteProductDtoSinId.Precio <= 0)
        {
            validationErrors.Add("El campo Precio debe ser mayor que cero.");
        }

        // Validación del campo Cantidad
        if (favoriteProductDtoSinId.Cantidad <= 0)
        {
            validationErrors.Add("El campo Cantidad debe ser mayor que cero.");
        }

        // Si hay errores de validación, agregamos los mensajes a la respuesta
        if (validationErrors.Any())
        {
            response.Errors.AddRange(validationErrors);
            return BadRequest(response);
        }

        FavoriteProductDto favoriteDto = new FavoriteProductDto()
        {
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
            response.Errors.Add("Category not found");
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

        // Validación del campo id
        if (favoritoDto.id == 0)
        {
            response.Errors.Add("El campo id no puede ser cero.");
        }
        
        // Validación del campo Image
        if (string.IsNullOrEmpty(favoritoDto.Image))
        {
            validationErrors.Add("El campo Image es obligatorio.");
        }

        // Validación del campo Titulo
        if (string.IsNullOrEmpty(favoritoDto.Titulo))
        {
            validationErrors.Add("El campo Titulo es obligatorio.");
        }

        // Validación del campo Precio
        if (favoritoDto.Precio <= 0)
        {
            validationErrors.Add("El campo Precio debe ser mayor que cero.");
        }

        // Validación del campo Cantidad
        if (favoritoDto.Cantidad <= 0)
        {
            validationErrors.Add("El campo Cantidad debe ser mayor que cero.");
        }

        // Si hay errores de validación, agregamos los mensajes a la respuesta
        if (validationErrors.Any())
        {
            response.Errors.AddRange(validationErrors);
            return BadRequest(response);
        }
        
        if (!await _favoriteProductServices.FavoriteProductExist(favoritoDto.id))
        {
            response.Errors.Add("Product Category not found");
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
            response.Errors.Add("id not found");
            return NotFound(response);
        }
        return Ok(response);
    }
}