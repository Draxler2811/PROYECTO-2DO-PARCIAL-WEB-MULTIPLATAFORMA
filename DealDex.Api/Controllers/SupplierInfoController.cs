using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using DealDex.Core.Entities;
using DealDex.Core.Http;
using DealDex.Api.Dto;
using DealDex.Api.Dto.Carrito;
using DealDex.Api.Dto.Supplier;
using DealDex.Api.Repositories.Interfecies;
using DealDex.Api.Services.Interfaces;
using Tecnm.Ecommerce1.Api.Repositories.Interfecies.Carrito;
using Tecnm.Ecommerce1.Api.Services.Interfaces.Carrito;
using Tecnm.Ecommerce1.Api.Services.Interfaces.Supplier;
using Tecnm.Ecommerce1.Api.Services.Interfaces.Users;
using Tecnm.Ecommerce1.Api.Services.Users;

namespace DealDex.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SupplierInfoController : ControllerBase
{
    private readonly ISupplierInfoService _supplierInfoService;


    public SupplierInfoController(ISupplierInfoService supplierInfoService)
    {

        _supplierInfoService = supplierInfoService;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<SupplierInfoDto>>>> GetAll()
    {
        var response = new Response<List<SupplierInfoDto>>
        {
            Data = await _supplierInfoService.GetAllAsync()
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<SupplierInfoDto>>> Post([FromBody] SupplierInfoDtoSinId supplierInfoDtoSinId)
    {
        var response = new Response<SupplierInfoDto>();

        // Lista para almacenar los mensajes de error
        var validationErrors = new List<string>();
        
        // Validación del campo Image
        if (string.IsNullOrEmpty(supplierInfoDtoSinId.Nombre))
        {
            validationErrors.Add("El campo Nombre es obligatorio.");
        }

        // Validación del campo Titulo
        if (string.IsNullOrEmpty(supplierInfoDtoSinId.Direccion))
        {
            validationErrors.Add("El campo Direccion es obligatorio.");
        }

        // Validación del campo Titulo
        if (string.IsNullOrEmpty(supplierInfoDtoSinId.Cuidad))
        {
            validationErrors.Add("El campo Cuidad es obligatorio.");
        }
        // Validación del campo Titulo
        if (string.IsNullOrEmpty(supplierInfoDtoSinId.Correo))
        {
            validationErrors.Add("El campo Correo es obligatorio.");
        }
        // Si hay errores de validación, agregamos los mensajes a la respuesta
        if (validationErrors.Any())
        {
            response.Errors.AddRange(validationErrors);
            return BadRequest(response);
        }

        // Si no hay errores de validación, procedemos con la creación del carrito
        SupplierInfoDto supplierInfoDto = new SupplierInfoDto()
        {
            Nombre =  supplierInfoDtoSinId.Nombre,
            Direccion =  supplierInfoDtoSinId.Direccion,
            Cuidad =  supplierInfoDtoSinId.Cuidad, 
            Correo=  supplierInfoDtoSinId.Correo,
        };

        response.Data = await _supplierInfoService.SaveAsycn(supplierInfoDto);
        return Created($"/api/[controller]/{response.Data.id}", response);
    }


    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<SupplierInfoDto>>> GetById(int id)
    {
        
        var response = new Response<SupplierInfoDto>();
        

        if (!await _supplierInfoService.SupplierInfoExist(id))
        {
            response.Errors.Add("Category not found");
            return NotFound(response);
        }


        response.Data = await _supplierInfoService.GetById(id); 
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<SupplierInfoDto>>> Update([FromBody] SupplierInfoDto supplierInfoDto)
    {
        var response = new Response<SupplierInfoDto>();

        // Lista para almacenar los mensajes de error
        var validationErrors = new List<string>();
       
        // Validación de ID
        if (supplierInfoDto.id <= 0)
        {
            validationErrors.Add("El ID debe ser mayor que cero.");
        }
   
        // Validación del campo Image
        if (string.IsNullOrEmpty(supplierInfoDto.Nombre))
        {
            validationErrors.Add("El campo Nombre es obligatorio.");
        }

        // Validación del campo Titulo
        if (string.IsNullOrEmpty(supplierInfoDto.Direccion))
        {
            validationErrors.Add("El campo Direccion es obligatorio.");
        }

        // Validación del campo Titulo
        if (string.IsNullOrEmpty(supplierInfoDto.Cuidad))
        {
            validationErrors.Add("El campo Cuidad es obligatorio.");
        }
        // Validación del campo Titulo
        if (string.IsNullOrEmpty(supplierInfoDto.Correo))
        {
            validationErrors.Add("El campo Correo es obligatorio.");
        }

        // Si hay errores de validación, agregamos los mensajes a la respuesta
        if (validationErrors.Any())
        {
            response.Errors.AddRange(validationErrors);
            return BadRequest(response);
        }

        // Si no hay errores de validación, procedemos con la actualización del carrito
        if (!await _supplierInfoService.SupplierInfoExist(supplierInfoDto.id))
        {
            response.Errors.Add("Product Category not found");
            return NotFound(response);
        }

        response.Data = await _supplierInfoService.UpdateAsync(supplierInfoDto);
        return Ok(response);
    }

    

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();

        if (!await _supplierInfoService.DeleteAsync(id))
        {
            response.Errors.Add("id not found");
            return NotFound(response);
        }
        return Ok(response);
    }
    }
