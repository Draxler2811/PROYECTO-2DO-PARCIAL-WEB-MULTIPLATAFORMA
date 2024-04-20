using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using DealDex.Core.Entities;
using DealDex.Core.Http;
using DealDex.Api.Dto;
using DealDex.Api.Repositories.Interfecies;
using DealDex.Api.Services.Interfaces;
using Tecnm.Ecommerce1.Api.Services.Interfaces.Users;

namespace DealDex.Api.Controllers;




[ApiController]
[Route("api/[controller]")]
public class UsersCategoriesController : ControllerBase
{
    
    private readonly IUsersCategoryService _usersCategoryService;
    
    public UsersCategoriesController(IUsersCategoryService usersCategoryService)
    {
        
        _usersCategoryService = usersCategoryService;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<UsersCategory>>>> GetAll()
    {
        var response = new Response<List<UsersCategoryDto>>
        {
            Data = await _usersCategoryService.GetAllAsync()
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<UsersCategoryDto>>> Post([FromBody] UserCategoryDtoSinId categoryDtoSinId)
    {
        var response = new Response<UsersCategoryDto>();

        if (string.IsNullOrEmpty(categoryDtoSinId.Correo))
        {
            response.Errors.Add("El campo Correo es obligatorio.");
        }
        if (string.IsNullOrEmpty(categoryDtoSinId.Contraseña))
        {
            response.Errors.Add("El campo Contraseña es obligatorio.");
        }
        if (string.IsNullOrEmpty(categoryDtoSinId.NombreUsu))
        {
            response.Errors.Add("El campo Nombre de usuario es obligatorio.");
        }

        if (response.Errors.Any())
        {
            return BadRequest(response);
        }

        UsersCategoryDto categoryDto = new UsersCategoryDto()
        {
            Correo = categoryDtoSinId.Correo,
            Contraseña = categoryDtoSinId.Contraseña,
            NombreUsu = categoryDtoSinId.NombreUsu
        };

        response.Data = await _usersCategoryService.SaveAsycn(categoryDto);
        return Created($"/api/[controller]/{response.Data.id}", response);
    }



    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<UsersCategoryDto>>> GetById(int id)
    {
        
        var response = new Response<UsersCategoryDto>();
        

        if (!await _usersCategoryService.UsersCategoryExist(id))
        {
            response.Errors.Add("Category not found");
            return NotFound(response);
        }


        response.Data = await _usersCategoryService.GetById(id); 
        return Ok(response);
    }
    [HttpPut]
    public async Task<ActionResult<Response<UsersCategoryDto>>> Update([FromBody] UsersCategoryDto categoryDto)
    {
        var response = new Response<UsersCategoryDto>();

        if (categoryDto.id <= 0)
        {
            response.Errors.Add("El campo ID debe ser mayor que cero.");
            return BadRequest(response);
        }

        if (string.IsNullOrEmpty(categoryDto.Correo))
        {
            response.Errors.Add("El campo Correo es obligatorio.");
        }
        if (string.IsNullOrEmpty(categoryDto.Contraseña))
        {
            response.Errors.Add("El campo Contraseña es obligatorio.");
        }
        if (string.IsNullOrEmpty(categoryDto.NombreUsu))
        {
            response.Errors.Add("El campo Nombre de usuario es obligatorio.");
        }

        if (response.Errors.Any())
        {
            return BadRequest(response);
        }

        if (!await _usersCategoryService.UsersCategoryExist(categoryDto.id))
        {
            response.Errors.Add("Product Category not found");
            return NotFound(response);
        }

        response.Data = await _usersCategoryService.UpdateAsync(categoryDto);
        return Ok(response);
    }


    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();
        // var category = await _productCategoryRepository.GetById(id);

        if (!await _usersCategoryService.DeleteAsync(id))
        {
            response.Errors.Add("id not found");
            return NotFound(response);
        }
        return Ok(response);
    }
    [HttpPost("login")]
    public async Task<ActionResult<Response<string>>> Login([FromBody] UserCategoryDtoValidar categoryDto)
    {
        
        
        var response = new Response<string>();

        if (string.IsNullOrEmpty(categoryDto.Correo) || string.IsNullOrEmpty(categoryDto.Contraseña))
        {
            response.Errors.Add("Correo y contraseña son obligatorios");
            return BadRequest(response);
        }

        var isValidLogin = await _usersCategoryService.ValidateCredentials(categoryDto.Correo, categoryDto.Contraseña);

        if (isValidLogin)
        {
            // Si las credenciales son válidas, devuelve un mensaje de éxito
            response.Data = "Credenciales correctas";
            return Ok(response);
        }
        else
        {
            response.Errors.Add("Usuario y/o contraseña incorrectos");
            return BadRequest(response);
        }
    }



}