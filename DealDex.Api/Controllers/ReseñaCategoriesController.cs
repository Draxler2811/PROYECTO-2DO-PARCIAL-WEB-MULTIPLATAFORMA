using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using DealDex.Core.Entities;
using DealDex.Core.Http;
using DealDex.Api.Dto;
using DealDex.Api.Dto.Carrito;
using DealDex.Api.Dto.Reseñas;
using DealDex.Api.Repositories.Interfecies;
using DealDex.Api.Services.Interfaces;
using Tecnm.Ecommerce1.Api.Services.Interfaces.Reseñas;

namespace DealDex.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductReviewController :ControllerBase
{
    
    
    private readonly IReseñasCategoryServices _reseñasCategoryServices;
    private readonly IProductCategoryService _productCategoryService;

    public ProductReviewController(IReseñasCategoryServices reseñasCategoryServices,IProductCategoryService productCategoryService)
    {
        
        _reseñasCategoryServices = reseñasCategoryServices;
        _productCategoryService = productCategoryService;

    }

    [HttpGet]
    public async Task<ActionResult<Response<List<ReseñaCategory>>>> GetAll()
    {
        var response = new Response<List<ReseñaCategoryDto>>();

        var reseñas = await _reseñasCategoryServices.GetAllAsync();

        if (reseñas == null || !reseñas.Any())
        {
            response.Errors.Add("No hay reseñas disponibles.");
            return NotFound(response);
        }

        response.Data = reseñas;
        return Ok(response);
    }
    [HttpPost]
    public async Task<ActionResult<Response<ReseñaCategoryDto>>> Post([FromBody] ReseñasCategoryDtoSinId categoryDtoSinId)
    {
        var response = new Response<ReseñaCategoryDto>();

        var validationErrors = new List<string>();

        if (await _reseñasCategoryServices.ExistByName(categoryDtoSinId.Titulo))
        {
            response.Errors.Add($"Titulo {categoryDtoSinId.Titulo} already exists");
            return BadRequest(response);
        }
        if (!await _productCategoryService.ProductCategoryExist(categoryDtoSinId.IdProducto))
        {
            validationErrors.Add("El producto relacionado no existe");
        }
        if (categoryDtoSinId.IdProducto == 0)
        {
            validationErrors.Add("El campo IdProducto es obligatorio.");
        }
    
        if (string.IsNullOrEmpty(categoryDtoSinId.Titulo))
        {
            validationErrors.Add("El campo Titulo es obligatorio.");
        }

        if (categoryDtoSinId.Valoracion <= 0 || categoryDtoSinId.Valoracion > 5)
        {
            validationErrors.Add("La Valoracion debe estar entre 1 y 5.");
        }

        if (validationErrors.Any())
        {
            response.Errors.AddRange(validationErrors);
            return BadRequest(response);
        }

        ReseñaCategoryDto categoryDto = new ReseñaCategoryDto
        {
            IdProducto = categoryDtoSinId.IdProducto,
            Titulo = categoryDtoSinId.Titulo,
            Valoracion = categoryDtoSinId.Valoracion
        };

        response.Data = await _reseñasCategoryServices.SaveAsycn(categoryDto);

        return Created($"/api/[controller]/{response.Data.id}", response);
    }




    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<ReseñaCategoryDto>>> GetById(int id)
    {
        var response = new Response<ReseñaCategoryDto>();
        if (!await _reseñasCategoryServices.ReseñaCategoryExist(id))
        {
            response.Errors.Add("El id de la reseña no fue encontrado");
            return NotFound(response);
        }
        response.Data = await _reseñasCategoryServices.GetById(id); 
        return Ok(response);
    }
    [HttpPut]
    public async Task<ActionResult<Response<ReseñaCategoryDto>>> Update([FromBody] ReseñaCategoryDto categoryDto)
    {
        var response = new Response<ReseñaCategoryDto>();

        var errors = new List<string>();
        
        if (!await _reseñasCategoryServices.ReseñaCategoryExist(categoryDto.id))
        {
            response.Errors.Add("Reseña not found");
            return NotFound(response);
        }
        if (await _reseñasCategoryServices.ExistByName(categoryDto.Titulo, categoryDto.id))
        {
            response.Errors.Add($"Titulo {categoryDto.Titulo} already exists");
            return BadRequest(response);
        }
        

        if (!await _productCategoryService.ProductCategoryExist(categoryDto.IdProducto))
        {
            errors.Add("La categoría asociada al IdProducto especificado no existe.");
        }
        else if (categoryDto.IdProducto == 0)
        {
            errors.Add("El campo IdProducto es obligatorio.");
        }
    
        if (categoryDto.id <= 0)
        {
            errors.Add("El ID debe ser mayor que cero.");
        }

        if (string.IsNullOrEmpty(categoryDto.Titulo))
        {
            errors.Add("El campo Titulo es obligatorio.");
        }

        if (categoryDto.Valoracion <= 0 || categoryDto.Valoracion > 5)
        {
            errors.Add("La Valoracion debe estar entre 1 y 5.");
        }

        if (errors.Any())
        {
            response.Errors.AddRange(errors);
            return BadRequest(response);
        }

        if (!await _reseñasCategoryServices.ReseñaCategoryExist(categoryDto.id))
        {
            response.Errors.Add("Id de la Reseña no fue encontrado");
            return NotFound(response);
        }

        response.Data = await _reseñasCategoryServices.UpdateAsync(categoryDto);
        return Ok(response);
    }




    

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();

        if (!await _reseñasCategoryServices.DeleteAsync(id))
        {
            response.Errors.Add("id no encontrado");
            return NotFound(response);
        }

        response.Message = "Borrado exitosamente";
        return Ok(response);
    }
}