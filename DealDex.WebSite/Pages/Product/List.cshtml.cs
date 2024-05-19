using DealDex.Api.Dto;
using DealDex.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DealDex.WebSite.Pages.Product;

public class ListModel : PageModel
{
    private readonly IProductService _service;
    
    public List<ProductCategoryDto> Brands { get; set; }

    public ListModel(IProductService service)
    {
        Brands = new List<ProductCategoryDto>();
        _service = service;
    }

    public async Task<IActionResult> OnGet()
    {
        var response = await _service.GetAllAsync();
        Brands = response.Data;
        return Page();
    }
}