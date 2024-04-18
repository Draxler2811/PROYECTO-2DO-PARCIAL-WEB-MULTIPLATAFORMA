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
    public async Task<ActionResult<Response<UsersCategoryDto>>> Post([FromBody] UsersCategoryDto categoryDto)
    {
        var response = new Response<UsersCategoryDto>()
        {
            Data = await _usersCategoryService.SaveAsycn(categoryDto)

        };
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
}