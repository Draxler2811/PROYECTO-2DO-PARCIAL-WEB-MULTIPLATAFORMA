using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using DealDex.Core.Entities;
using DealDex.Core.Http;
using DealDex.Api.Dto;
using DealDex.Api.Dto.Reseñas;
using DealDex.Api.Repositories.Interfecies;
using DealDex.Api.Services.Interfaces;
using Tecnm.Ecommerce1.Api.Services.Interfaces.Reseñas;

namespace DealDex.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ReseñaCategoriesController :ControllerBase
{
    
    
    private readonly IReseñasCategoryServices _reseñasCategoryServices;
    private readonly IProductCategoryService _productCategoryService;

    public ReseñaCategoriesController(IReseñasCategoryServices reseñasCategoryServices,IProductCategoryService productCategoryService)
    {
        
        _reseñasCategoryServices = reseñasCategoryServices;
        _productCategoryService = productCategoryService;

    }

    [HttpGet]
    public async Task<ActionResult<Response<List<ReseñaCategory>>>> GetAll()
    {
        var response = new Response<List<ReseñaCategoryDto>>
        {
            Data = await _reseñasCategoryServices.GetAllAsync()
        };
        return Ok(response);
    }
    [HttpPost]
    public async Task<ActionResult<Response<ReseñaCategoryDto>>> Post([FromBody] ReseñasCategoryDtoSinId categoryDtoSinId)
    {
        var response = new Response<ReseñaCategoryDto>();

        // Lista para almacenar los mensajes de error
        var validationErrors = new List<string>();

        // Verificar si la categoría asociada a IdCategory existe
        if (!await _productCategoryService.ProductCategoryExist(categoryDtoSinId.IdProducto))
        {
            response.Errors.Add("La categoría asociada al IdCategory especificado no existe.");
            return BadRequest(response);
        }
        
        // Validar que IdCategory no sea cero
        if (categoryDtoSinId.IdProducto == 0)
        {
            response.Errors.Add("El campo IdCategory es obligatorio.");
            return BadRequest(response);
        }
        
        
        // Validación del campo Titulo
        if (string.IsNullOrEmpty(categoryDtoSinId.Titulo))
        {
            validationErrors.Add("El campo Titulo es obligatorio.");
        }

        // Validación del campo Valoracion
        if (categoryDtoSinId.Valoracion <= 0 || categoryDtoSinId.Valoracion > 5)
        {
            validationErrors.Add("La Valoracion debe estar entre 1 y 5.");
        }

        // Si hay errores de validación, devolver una respuesta de error con todos los mensajes
        if (validationErrors.Any())
        {
            response.Errors.AddRange(validationErrors);
            return BadRequest(response);
        }

        // Mapeo del DTO
        ReseñaCategoryDto  categoryDto = new ReseñaCategoryDto
        {
            IdProducto = categoryDtoSinId.IdProducto,
            Titulo = categoryDtoSinId.Titulo,
            Valoracion = categoryDtoSinId.Valoracion
        };

        // Guardando la nueva reseña usando un servicio
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
            response.Errors.Add("Category not found");
            return NotFound(response);
        }


        response.Data = await _reseñasCategoryServices.GetById(id); 
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<ReseñaCategoryDto>>> Update([FromBody] ReseñaCategoryDto categoryDto)
    {
        var response = new Response<ReseñaCategoryDto>();

        // Verificar si la categoría asociada a IdCategory existe
        if (!await _productCategoryService.ProductCategoryExist(categoryDto.IdProducto))
        {
            response.Errors.Add("La categoría asociada al IdCategory especificado no existe.");
            return BadRequest(response);
        }
        
        // Validar que IdCategory no sea cero
        if (categoryDto.IdProducto == 0)
        {
            response.Errors.Add("El campo IdCategory es obligatorio.");
            return BadRequest(response);
        }
        
        // Validación del ID
        if (categoryDto.id <= 0)
        {
            response.Errors.Add("El ID debe ser mayor que cero.");
        }

        // Lista para almacenar los mensajes de error
        var validationErrors = new List<string>();

        // Validación del campo Titulo
        if (string.IsNullOrEmpty(categoryDto.Titulo))
        {
            validationErrors.Add("El campo Titulo es obligatorio.");
        }

        // Validación del campo Valoracion
        if (categoryDto.Valoracion <= 0 || categoryDto.Valoracion > 5)
        {
            validationErrors.Add("La Valoracion debe estar entre 1 y 5.");
        }

        // Si hay errores de validación, agregamos los mensajes a la respuesta
        if (response.Errors.Any() || validationErrors.Any())
        {
            response.Errors.AddRange(validationErrors);
            return BadRequest(response);
        }

        // Si el ID es válido y no hay errores de validación, procedemos con la actualización
        if (!await _reseñasCategoryServices.ReseñaCategoryExist(categoryDto.id))
        {
            response.Errors.Add("Reseña Category not found");
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
            response.Errors.Add("id not found");
            return NotFound(response);
        }
        return Ok(response);
    }
}