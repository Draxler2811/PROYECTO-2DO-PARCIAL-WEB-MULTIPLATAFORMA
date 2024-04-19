using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using DealDex.Core.Entities;
using DealDex.Core.Http;
using DealDex.Api.Dto;
using DealDex.Api.Dto.Carrito;
using DealDex.Api.Dto.Categories;
using DealDex.Api.Repositories.Interfecies;
using DealDex.Api.Services.Interfaces;
using Tecnm.Ecommerce1.Api.Repositories.Interfecies.Carrito;
using Tecnm.Ecommerce1.Api.Services.Interfaces.category;

namespace DealDex.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryTypeController : ControllerBase
{
    private readonly ICategoryTypeServices _categoryTypeServices;
    
    public CategoryTypeController(ICategoryTypeServices categoryTypeServices)
    {
        
        _categoryTypeServices = categoryTypeServices;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<CategoryTypeDto>>>> GetAll()
    {
        var response = new Response<List<CategoryTypeDto>>
        {
            Data = await _categoryTypeServices.GetAllAsync()
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<CategoryTypeDto>>> Post([FromBody] CategoryTypeDto carritoDto)
    {
        var response = new Response<CategoryTypeDto>()
        {
            Data = await _categoryTypeServices.SaveAsyc(carritoDto)

        };
        return Created($"/api/[controller]/{response.Data.id}", response);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<CategoryTypeDto>>> GetById(int id)
    {
        
        var response = new Response<CategoryTypeDto>();
        

        if (!await _categoryTypeServices.CategoryTypeExist(id))
        {
            response.Errors.Add("Category not found");
            return NotFound(response);
        }


        response.Data = await _categoryTypeServices.GetById(id); 
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<CategoryTypeDto>>> Update([FromBody] CategoryTypeDto carritoDto)
    {
        var response = new Response<CategoryTypeDto>();
        if (!await _categoryTypeServices.CategoryTypeExist(carritoDto.id))
        {
            response.Errors.Add("Product Category not found");
            return NotFound(response);
        }

        response.Data = await _categoryTypeServices.UpdateAsync(carritoDto);
        return Ok(response);

    }
    

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();

        if (!await _categoryTypeServices.DeleteAsync(id))
        {
            response.Errors.Add("id not found");
            return NotFound(response);
        }
        return Ok(response);
    }
}