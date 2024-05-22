using DealDex.Api.Dto;
using DealDex.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DealDex.WebSite.Pages.Product;

public class ListModel : PageModel
{
    private readonly IProductService _service;
    
    public List<ProductCategoryDtoAdd> Product { get; set; }

    public ListModel(IProductService service)
    {
        Product = new List<ProductCategoryDtoAdd>();
        _service = service;
    }

    public async Task<IActionResult> OnGet()
    {
        var response = await _service.GetAllAsync();
        Product = response.Data;
        return Page();
    }
}