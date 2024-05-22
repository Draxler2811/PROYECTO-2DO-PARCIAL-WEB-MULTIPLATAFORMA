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
public class CategoryProductController : ControllerBase
{
    private readonly ICategoryTypeServices _categoryTypeServices;
    
    public CategoryProductController(ICategoryTypeServices categoryTypeServices)
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

        if (response.Data == null || !response.Data.Any())
        {
            response.Errors.Add("No se encontraron datos.");
            return NotFound(response);
        }

        return Ok(response);
    }
    [HttpPost]
    public async Task<ActionResult<Response<CategoryTypeDto>>> Post([FromBody] CategoryTypeDtoSinId categoryTypeDtoSinId)
    {
        var response = new Response<CategoryTypeDto>();
        
        if (await _categoryTypeServices.ExistByName(categoryTypeDtoSinId.Nombre))
        {
            response.Errors.Add($"Brand name {categoryTypeDtoSinId.Nombre} already exists");
            return BadRequest(response);
        }
        
        if (string.IsNullOrEmpty(categoryTypeDtoSinId.Nombre))
        {
            response.Errors.Add("El campo Nombre es obligatorio.");
            return BadRequest(response);
        }

        CategoryTypeDto categoryTypeDto = new CategoryTypeDto()
        {
            Nombre = categoryTypeDtoSinId.Nombre,
        };

        response.Data = await _categoryTypeServices.SaveAsyc(categoryTypeDto);
        return Created($"/api/[controller]/{response.Data.id}", response);
    }


    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<CategoryTypeDto>>> GetById(int id)
    {
        
        var response = new Response<CategoryTypeDto>();
        if (!await _categoryTypeServices.CategoryTypeExist(id))
        {
            response.Errors.Add("El id de la categoria no fue encontrado");
            return NotFound(response);
        }
        response.Data = await _categoryTypeServices.GetById(id); 
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<CategoryTypeDto>>> Update([FromBody] CategoryTypeDto categoryTypeDto)
    {
        var response = new Response<CategoryTypeDto>();

        if (!await _categoryTypeServices.CategoryTypeExist(categoryTypeDto.id))
        {
            response.Errors.Add("Product Category not found");
            return NotFound(response);
        }
        if (await _categoryTypeServices.ExistByName(categoryTypeDto.Nombre, categoryTypeDto.id))
        {
            response.Errors.Add($"Product Brand Name {categoryTypeDto.Nombre} already exists");
            return BadRequest(response);
        }
        
        if (categoryTypeDto.id == 0)
        {
            response.Errors.Add("El campo id no puede ser cero.");
        }

        if (string.IsNullOrEmpty(categoryTypeDto.Nombre))
        {
            response.Errors.Add("El campo Nombre es obligatorio.");
        }

        if (response.Errors.Any())
        {
            return BadRequest(response);
        }

        if (!await _categoryTypeServices.CategoryTypeExist(categoryTypeDto.id))
        {
            response.Errors.Add("El id de la categoria no fue encontrado");
            return NotFound(response);
        }

        response.Data = await _categoryTypeServices.UpdateAsync(categoryTypeDto);
        return Ok(response);
    }

    

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();

        if (!await _categoryTypeServices.DeleteAsync(id))
        {
            response.Errors.Add("id no encontrado");
            return NotFound(response);
        }

        response.Message = "Borrado exitosamente";
        return Ok(response);
    }
}