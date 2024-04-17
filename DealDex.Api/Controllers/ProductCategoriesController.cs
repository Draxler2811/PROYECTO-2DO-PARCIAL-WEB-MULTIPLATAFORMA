using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using DealDex.Core.Entities;
using DealDex.Core.Http;
using DealDex.Api.Dto;
using DealDex.Api.Repositories.Interfecies;
using DealDex.Api.Services.Interfaces;

namespace DealDex.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductCategoriesController : ControllerBase
{
    // private readonly IProductCategoryRepository _productCategoryRepository;
    private readonly IProductCategoryService _productCategoryService;
    
    public ProductCategoriesController(IProductCategoryService productCategoryService)
    {
        // _productCategoryRepository = productCategoryRepository;
        _productCategoryService = productCategoryService;
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
    public async Task<ActionResult<Response<ProductCategoryDtoAdd>>> Post([FromBody] ProductCategoryDtoAdd categoryDto)
    {
        var response = new Response<ProductCategoryDtoAdd>()
        {
            Data = await _productCategoryService.SaveAsycn(categoryDto)

        };
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
        if (!await _productCategoryService.ProductCategoryExist(categoryDto.id))
        {
            response.Errors.Add("Product Category not found");
            return NotFound(response);
        }

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