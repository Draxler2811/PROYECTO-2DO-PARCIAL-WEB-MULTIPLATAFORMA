using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using DealDex.Core.Entities;
using DealDex.Core.Http;
using DealDex.Api.Dto;
using DealDex.Api.Dto.Carrito;
using DealDex.Api.Repositories.Interfecies;
using DealDex.Api.Services.Interfaces;
using Tecnm.Ecommerce1.Api.Repositories.Interfecies.Carrito;
using Tecnm.Ecommerce1.Api.Services.Interfaces.Carrito;
using Tecnm.Ecommerce1.Api.Services.Interfaces.Users;
using Tecnm.Ecommerce1.Api.Services.Users;

namespace DealDex.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
     private readonly ICarritoCategoryServices _carritoCategoryServices;
     private readonly IUsersCategoryService _usersCategoryService;
     private readonly IProductCategoryService _productCategoryService;

    public CartController(ICarritoCategoryServices carritoCategoryServices,IUsersCategoryService usersCategoryService,
        IProductCategoryService productCategoryService)
    {
        _productCategoryService = productCategoryService;
        _carritoCategoryServices = carritoCategoryServices;
        _usersCategoryService = usersCategoryService;

    }

    [HttpGet]
    public async Task<ActionResult<Response<List<CarritoCategoryDto>>>> GetAll()
    {
        var response = new Response<List<CarritoCategoryDto>>();

        var categories = await _carritoCategoryServices.GetAllAsync();

        if (categories == null || categories.Count == 0)
        {
            response.Errors.Add("No se encontraron datos.");
            return NotFound(response);
        }

        response.Data = categories;
        return Ok(response);
    }

   [HttpPost]
public async Task<ActionResult<Response<CarritoCategoryDto>>> Post([FromBody] CarritoCategoryDtoSinAdd carritoCategoryDtoSinAdd)
{
    var response = new Response<CarritoCategoryDto>();

    var validationErrors = new List<string>();
    
    if (!await _usersCategoryService.UsersCategoryExist(carritoCategoryDtoSinAdd.IdUser))
    {
        validationErrors.Add("El usuarios asociado no existe");
    }
    
    if (carritoCategoryDtoSinAdd.IdUser == 0)
    {
        validationErrors.Add("El campo IdUser es obligatorio.");
    }
    if (!await _productCategoryService.ProductCategoryExist(carritoCategoryDtoSinAdd.IdProducto))
    {
        validationErrors.Add("El producto relacionado no existe");
    }
    
    if (carritoCategoryDtoSinAdd.IdProducto == 0)
    {
        validationErrors.Add("El campo IdProducto es obligatorio.");
    }

    if (string.IsNullOrEmpty(carritoCategoryDtoSinAdd.Image))
    {
        validationErrors.Add("El campo Image es obligatorio.");
    }

    if (string.IsNullOrEmpty(carritoCategoryDtoSinAdd.Titulo))
    {
        validationErrors.Add("El campo Titulo es obligatorio.");
    }

    if (carritoCategoryDtoSinAdd.Precio <= 0)
    {
        validationErrors.Add("El campo Precio debe ser mayor que cero.");
    }

    if (carritoCategoryDtoSinAdd.Cantidad <= 0)
    {
        validationErrors.Add("El campo Cantidad debe ser mayor que cero.");
    }

    if (validationErrors.Any())
    {
        response.Errors.AddRange(validationErrors);
        return BadRequest(response);
    }

    CarritoCategoryDto carritoDto = new CarritoCategoryDto()
    {
        IdProducto = carritoCategoryDtoSinAdd.IdProducto,
        IdUser = carritoCategoryDtoSinAdd.IdUser,
        Image = carritoCategoryDtoSinAdd.Image,
        Titulo = carritoCategoryDtoSinAdd.Titulo,
        Precio = carritoCategoryDtoSinAdd.Precio,
        Cantidad = carritoCategoryDtoSinAdd.Cantidad
    };

    response.Data = await _carritoCategoryServices.SaveAsyc(carritoDto);
    return Created($"/api/[controller]/{response.Data.id}", response);
}


    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<CarritoCategoryDto>>> GetById(int id)
    {
        
        var response = new Response<CarritoCategoryDto>();
        

        if (!await _carritoCategoryServices.CarritoCategoryExist(id))
        {
            response.Errors.Add("Id del carrito no encontrado");
            return NotFound(response);
        }


        response.Data = await _carritoCategoryServices.GetById(id); 
        return Ok(response);
    }

   [HttpPut]
public async Task<ActionResult<Response<CarritoCategoryDto>>> Update([FromBody] CarritoCategoryDto carritoDto)
{
    var response = new Response<CarritoCategoryDto>();

    var validationErrors = new List<string>();

    if (!await _usersCategoryService.UsersCategoryExist(carritoDto.IdUser))
    {
        validationErrors.Add("La categoría asociada al IdCategory especificado no existe.");
    }
    
    if (carritoDto.IdUser == 0)
    {
        validationErrors.Add("El campo IdCategory es obligatorio.");
    }
    if (!await _productCategoryService.ProductCategoryExist(carritoDto.IdProducto))
    {
        validationErrors.Add("El id Producto especificado no existe.");
    }
    
    if (carritoDto.IdProducto == 0)
    {
        validationErrors.Add("El campo IdProducto es obligatorio.");
    }
    if (carritoDto.id == 0)
    {
        validationErrors.Add("El campo ID no puede ser cero.");
    }

    if (string.IsNullOrEmpty(carritoDto.Image))
    {
        validationErrors.Add("El campo Image es obligatorio.");
    }

    if (string.IsNullOrEmpty(carritoDto.Titulo))
    {
        validationErrors.Add("El campo Titulo es obligatorio.");
    }

    if (carritoDto.Precio <= 0)
    {
        validationErrors.Add("El campo Precio debe ser mayor que cero.");
    }

    if (carritoDto.Cantidad <= 0)
    {
        validationErrors.Add("El campo Cantidad debe ser mayor que cero.");
    }

    if (validationErrors.Any())
    {
        response.Errors.AddRange(validationErrors);
        return BadRequest(response);
    }

    if (!await _carritoCategoryServices.CarritoCategoryExist(carritoDto.id))
    {
        response.Errors.Add(" IdCarrito no encontrado");
        return NotFound(response);
    }

    response.Data = await _carritoCategoryServices.UpdateAsync(carritoDto);
    return Ok(response);
}


    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();

        if (!await _carritoCategoryServices.DeleteAsync(id))
        {
            response.Errors.Add("id no encontrado");
            return NotFound(response);
        }
        response.Message = "Borrado con exitoso";
        return Ok(response);
    }
}