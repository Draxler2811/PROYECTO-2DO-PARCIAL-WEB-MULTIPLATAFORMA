using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using DealDex.Core.Entities;
using DealDex.Core.Http;
using DealDex.Api.Dto;
using DealDex.Api.Repositories.Interfecies;
using DealDex.Api.Services.Interfaces;
using Tecnm.Ecommerce1.Api.Services.Interfaces.category;

namespace DealDex.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductCategoriesController : ControllerBase
{
    // private readonly IProductCategoryRepository _productCategoryRepository;
    private readonly IProductCategoryService _productCategoryService;
    private readonly ICategoryTypeServices _categoryTypeServices;
    
    public ProductCategoriesController(IProductCategoryService productCategoryService, ICategoryTypeServices categoryTypeServices)
    {
        _productCategoryService = productCategoryService;
        _categoryTypeServices = categoryTypeServices;
    }


    [HttpGet]
    public async Task<ActionResult<Response<List<ProductCategory>>>> GetAll()
    {
        var response = new Response<List<ProductCategoryDto>>
        {
            Data = await _productCategoryService.GetAllAsync()
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<ProductCategoryDtoAdd>>> Post([FromBody] ProductCategoryDtoSinId categoryDtoSinId)
    {
        var response = new Response<ProductCategoryDtoAdd>();

        // Verificar si la categoría asociada a IdCategory existe
        if (!await _categoryTypeServices.CategoryTypeExist(categoryDtoSinId.IdCategory))
        {
            response.Errors.Add("La categoría asociada al IdCategory especificado no existe.");
            return BadRequest(response);
        }
        
        // Validar que IdCategory no sea cero
        if (categoryDtoSinId.IdCategory == 0)
        {
            response.Errors.Add("El campo IdCategory es obligatorio.");
            return BadRequest(response);
        }
        
        if (string.IsNullOrEmpty(categoryDtoSinId.Image))
        {
            response.Errors.Add("El campo Image es obligatorio.");
        }
        if (string.IsNullOrEmpty(categoryDtoSinId.Titulo))
        {
            response.Errors.Add("El campo Titulo es obligatorio.");
        }
        if (categoryDtoSinId.Precio <= 0)
        {
            response.Errors.Add("El campo Precio debe ser mayor que cero.");
        }
        if (string.IsNullOrEmpty(categoryDtoSinId.Descripcion))
        {
            response.Errors.Add("El campo Descripcion es obligatorio.");
        }
        
        if (string.IsNullOrEmpty(categoryDtoSinId.Estado))
        {
            response.Errors.Add("El campo Estado es obligatorio.");
        }
        if (string.IsNullOrEmpty(categoryDtoSinId.Ubicacion))
        {
            response.Errors.Add("El campo Ubicacion es obligatorio.");
        }

        if (response.Errors.Any())
        {
            return BadRequest(response);
        }

        // Mapeo del DTO
        ProductCategoryDtoAdd categoryDto = new ProductCategoryDtoAdd()
        {
            IdCategory = categoryDtoSinId.IdCategory,
            Descripcion = categoryDtoSinId.Descripcion,
            Estado = categoryDtoSinId.Estado,
            Ubicacion = categoryDtoSinId.Ubicacion,
            Image = categoryDtoSinId.Image,
            Titulo = categoryDtoSinId.Titulo,
            Precio = categoryDtoSinId.Precio,
        };

        // Guardando la nueva categoría de producto usando un servicio
        response.Data = await _productCategoryService.SaveAsycn(categoryDto);
        return Created($"/api/[controller]/{response.Data.id}", response);
    }




    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<ProductCategoryDto>>> GetById(int id)
    {
        
        var response = new Response<ProductCategoryDtoById>();
        // var category = await _productCategoryRepository.GetById(id);

        if (!await _productCategoryService.ProductCategoryExist(id))
        {
            response.Errors.Add("Category not found");
            return NotFound(response);
        }


        response.Data = await _productCategoryService.GetById(id); 
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<ProductCategoryDtoAdd>>> Update([FromBody] ProductCategoryDtoAdd categoryDto)
    {
        var response = new Response<ProductCategoryDtoAdd>();

        // Lista para almacenar los errores de validación
        var validationErrors = new List<string>();

        // Verificar si la categoría asociada a IdCategory existe
        if (!await _categoryTypeServices.CategoryTypeExist(categoryDto.IdCategory))
        {
            response.Errors.Add("La categoría asociada al IdCategory especificado no existe.");
            return BadRequest(response);
        }
        
        // Validar que IdCategory no sea cero
        if (categoryDto.IdCategory == 0)
        {
            response.Errors.Add("El campo IdCategory es obligatorio.");
            return BadRequest(response);
        }
        
        // Validación de ID
        if (categoryDto.id <= 0)
        {
            validationErrors.Add("El ID debe ser mayor que cero.");
        }

        // Validación de campos no vacíos
        if (string.IsNullOrWhiteSpace(categoryDto.Image))
        {
            validationErrors.Add("El campo Image es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(categoryDto.Titulo))
        {
            validationErrors.Add("El campo Titulo es obligatorio.");
        }

        if (categoryDto.Precio <= 0)
        {
            validationErrors.Add("El campo Precio debe ser mayor que cero.");
        }

        if (string.IsNullOrWhiteSpace(categoryDto.Descripcion))
        {
            validationErrors.Add("El campo Descripcion es obligatorio.");
        }

      

        if (string.IsNullOrWhiteSpace(categoryDto.Estado))
        {
            validationErrors.Add("El campo Estado es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(categoryDto.Ubicacion))
        {
            validationErrors.Add("El campo Ubicacion es obligatorio.");
        }

        // Validación de existencia de la categoría
        if (!await _productCategoryService.ProductCategoryExist(categoryDto.id))
        {
            validationErrors.Add("Product Category not found");
        }

        // Si hay errores de validación, devolverlos todos juntos
        if (validationErrors.Any())
        {
            response.Errors.AddRange(validationErrors);
            return BadRequest(response);
        }

        // Actualización de la categoría si pasa todas las validaciones
        response.Data = await _productCategoryService.UpdateAsync(categoryDto);
        return Ok(response);
    }


    

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();
        // var category = await _productCategoryRepository.GetById(id);

        if (!await _productCategoryService.DeleteAsync(id))
        {
            response.Errors.Add("id not found");
            return NotFound(response);
        }
        return Ok(response);
    }
}