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
    public async Task<ActionResult<Response<FavoriteProductDto>>> Post([FromBody] FavoriteProductDto carritoDto)
    {
        var response = new Response<FavoriteProductDto>()
        {
            Data = await _favoriteProductServices.SaveAsyc(carritoDto)

        };
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
    public async Task<ActionResult<Response<FavoriteProductDto>>> Update([FromBody] FavoriteProductDto carritoDto)
    {
        var response = new Response<FavoriteProductDto>();
        if (!await _favoriteProductServices.FavoriteProductExist(carritoDto.id))
        {
            response.Errors.Add("Product Category not found");
            return NotFound(response);
        }

        response.Data = await _favoriteProductServices.UpdateAsync(carritoDto);
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