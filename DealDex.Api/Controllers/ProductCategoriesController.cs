using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using DealDex.Core.Entities;
using DealDex.Core.Http;
using DealDex.Api.Dto;
using DealDex.Api.Repositories.Interfecies;
using DealDex.Api.Services.Interfaces;
using Tecnm.Ecommerce1.Api.Services.Interfaces.category;
using Tecnm.Ecommerce1.Api.Services.Interfaces.Supplier;

namespace DealDex.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductCategoryService _productCategoryService;
    private readonly ICategoryTypeServices _categoryTypeServices;
    private readonly ISupplierInfoService _supplierInfoService;

    
    public ProductController(IProductCategoryService productCategoryService, ICategoryTypeServices categoryTypeServices,
        ISupplierInfoService supplierInfoService)
    {
        _productCategoryService = productCategoryService;
        _categoryTypeServices = categoryTypeServices;
        _supplierInfoService = supplierInfoService;

    }


    [HttpGet]
    public async Task<ActionResult<Response<List<ProductCategoryDtoAdd>>>> GetAll()
    {
        var response = new Response<List<ProductCategoryDtoAdd>>
        {
            Data = await _productCategoryService.GetAllAsync()
        };

        if (response.Data == null || !response.Data.Any())
        {
            response.Errors.Add("No se encontraron datos.");
            return NotFound(response);
        }

        return Ok(response);
    }

  [HttpPost]
public async Task<ActionResult<Response<ProductCategoryDtoAdd>>> Post([FromBody] ProductCategoryDtoSinId categoryDtoSinId)
{
    var response = new Response<ProductCategoryDtoAdd>();

    var validationErrors = new List<string>();
    
    if (await _productCategoryService.ExistByName(categoryDtoSinId.Titulo))
    {
        response.Errors.Add($"Product Category name {categoryDtoSinId.Titulo} already exists");
        return BadRequest(response);
    }

    if (categoryDtoSinId.IdCategory == 0)
    {
        validationErrors.Add("El campo IdCategory es obligatorio.");
    }
    
    if (!await _supplierInfoService.SupplierInfoExist(categoryDtoSinId.IdSupplier))
    {
        validationErrors.Add("La categoría asociada al IdSupplier especificado no existe.");
    }

    if (categoryDtoSinId.IdSupplier == 0)
    {
        validationErrors.Add("El campo IdSupplier es obligatorio.");
    }
    
    if (string.IsNullOrEmpty(categoryDtoSinId.Image))
    {
        validationErrors.Add("El campo Image es obligatorio.");
    }
    if (string.IsNullOrEmpty(categoryDtoSinId.Titulo))
    {
        validationErrors.Add("El campo Titulo es obligatorio.");
    }
    if (categoryDtoSinId.Precio <= 0)
    {
        validationErrors.Add("El campo Precio debe ser mayor que cero.");
    }
    if (string.IsNullOrEmpty(categoryDtoSinId.Descripcion))
    {
        validationErrors.Add("El campo Descripcion es obligatorio.");
    }
    
    if (string.IsNullOrEmpty(categoryDtoSinId.Estado))
    {
        validationErrors.Add("El campo Estado es obligatorio.");
    }
    if (string.IsNullOrEmpty(categoryDtoSinId.Ubicacion))
    {
        validationErrors.Add("El campo Ubicacion es obligatorio.");
    }

    if (validationErrors.Any())
    {
        response.Errors.AddRange(validationErrors);
        return BadRequest(response);
    }

    ProductCategoryDtoAdd categoryDto = new ProductCategoryDtoAdd()
    {
        IdSupplier = categoryDtoSinId.IdSupplier,
        IdCategory = categoryDtoSinId.IdCategory,
        Descripcion = categoryDtoSinId.Descripcion,
        Estado = categoryDtoSinId.Estado,
        Ubicacion = categoryDtoSinId.Ubicacion,
        Image = categoryDtoSinId.Image,
        Titulo = categoryDtoSinId.Titulo,
        Precio = categoryDtoSinId.Precio,
    };

    response.Data = await _productCategoryService.SaveAsycn(categoryDto);
    return Created($"/api/[controller]/{response.Data.id}", response);
}




    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<ProductCategoryDtoAdd>>> GetById(int id)
    {
        
        var response = new Response<ProductCategoryDtoAdd>();

        if (!await _productCategoryService.ProductCategoryExist(id))
        {
            response.Errors.Add("El id del producto no fue encontrado");
            return NotFound(response);
        }
        response.Data = await _productCategoryService.GetById(id); 
        return Ok(response);
    }

   [HttpPut]
public async Task<ActionResult<Response<ProductCategoryDtoAdd>>> Update([FromBody] ProductCategoryDtoAdd categoryDto)
{
    var response = new Response<ProductCategoryDtoAdd>();

    var validationErrors = new List<string>();
    
    if (!await _productCategoryService.ProductCategoryExist(categoryDto.id))
    {
        response.Errors.Add("Product Category not found");
        return NotFound(response);
    }
    if (await _productCategoryService.ExistByName(categoryDto.Titulo, categoryDto.id))
    {
        response.Errors.Add($"Product Brand Name {categoryDto.Titulo} already exists");
        return BadRequest(response);
    }
    
    
    if (!await _categoryTypeServices.CategoryTypeExist(categoryDto.IdCategory))
    {
        validationErrors.Add("La categoría asociada al IdCategory especificado no existe.");
    }

    if (!await _categoryTypeServices.CategoryTypeExist(categoryDto.IdSupplier))
    {
        validationErrors.Add("La categoría asociada al IdSupplier especificado no existe.");
    }
            
    if (categoryDto.IdSupplier == 0)
    {
        validationErrors.Add("El campo IdSupplier es obligatorio.");
    }

    if (categoryDto.IdCategory == 0)
    {
        validationErrors.Add("El campo IdCategory es obligatorio.");
    }
    
    if (categoryDto.id <= 0)
    {
        validationErrors.Add("El ID debe ser mayor que cero.");
    }

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

    if (!await _productCategoryService.ProductCategoryExist(categoryDto.id))
    {
        validationErrors.Add("Id del producto no fue encontrado");
    }

    if (validationErrors.Any())
    {
        response.Errors.AddRange(validationErrors);
        return BadRequest(response);
    }

    response.Data = await _productCategoryService.UpdateAsync(categoryDto);
    return Ok(response);
}


    

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();

        if (!await _productCategoryService.DeleteAsync(id))
        {
            response.Errors.Add("id no encontrado");
            return NotFound(response);
        }

        response.Message = "Borrado exitosamente";
        return Ok(response);
    }
}