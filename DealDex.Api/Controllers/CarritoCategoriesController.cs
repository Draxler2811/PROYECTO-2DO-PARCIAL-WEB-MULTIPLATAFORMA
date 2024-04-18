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

namespace DealDex.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CarritoCategoriesController : ControllerBase
{
     private readonly ICarritoCategoryServices _carritoCategoryServices;
    
    public CarritoCategoriesController(ICarritoCategoryServices carritoCategoryServices)
    {
        
        _carritoCategoryServices = carritoCategoryServices;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<CarritoCategoryDto>>>> GetAll()
    {
        var response = new Response<List<CarritoCategoryDto>>
        {
            Data = await _carritoCategoryServices.GetAllAsync()
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<CarritoCategoryDto>>> Post([FromBody] CarritoCategoryDto carritoDto)
    {
        var response = new Response<CarritoCategoryDto>()
        {
            Data = await _carritoCategoryServices.SaveAsyc(carritoDto)

        };
        return Created($"/api/[controller]/{response.Data.id}", response);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<CarritoCategoryDto>>> GetById(int id)
    {
        
        var response = new Response<CarritoCategoryDto>();
        

        if (!await _carritoCategoryServices.CarritoCategoryExist(id))
        {
            response.Errors.Add("Category not found");
            return NotFound(response);
        }


        response.Data = await _carritoCategoryServices.GetById(id); 
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<CarritoCategoryDto>>> Update([FromBody] CarritoCategoryDto carritoDto)
    {
        var response = new Response<CarritoCategoryDto>();
        if (!await _carritoCategoryServices.CarritoCategoryExist(carritoDto.id))
        {
            response.Errors.Add("Product Category not found");
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
            response.Errors.Add("id not found");
            return NotFound(response);
        }
        return Ok(response);
    }
}