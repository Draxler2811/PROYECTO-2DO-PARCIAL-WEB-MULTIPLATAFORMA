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
    
    public ReseñaCategoriesController(IReseñasCategoryServices reseñasCategoryServices)
    {
        
        _reseñasCategoryServices = reseñasCategoryServices;
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
    public async Task<ActionResult<Response<ReseñaCategoryDto>>> Post([FromBody] ReseñaCategoryDto categoryDto)
    {
        var response = new Response<ReseñaCategoryDto>()
        {
            Data = await _reseñasCategoryServices.SaveAsycn(categoryDto)

        };
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
        if (!await _reseñasCategoryServices.ReseñaCategoryExist(categoryDto.id))
        {
            response.Errors.Add("Product Category not found");
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