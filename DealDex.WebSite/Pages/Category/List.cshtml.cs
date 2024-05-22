using DealDex.Api.Dto.Carrito;
using DealDex.Api.Dto.Categories;
using DealDex.WebSite.Services;
using DealDex.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DealDex.WebSite.Pages.Category;

public class ListModel : PageModel
{
    private readonly ICategoryService _service;

    public List<CategoryTypeDto> CategoryTypeDto{ get; set; }

    public ListModel(ICategoryService service)
    {
        CategoryTypeDto = new List<CategoryTypeDto>();
        _service = service;
    }

    public async Task<IActionResult> OnGet()
    {
        var response = await _service.GetAllAsync();
        CategoryTypeDto = response.Data;
        return Page();
    }
}