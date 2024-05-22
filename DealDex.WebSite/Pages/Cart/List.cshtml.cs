using DealDex.Api.Dto;
using DealDex.Api.Dto.Carrito;
using DealDex.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DealDex.WebSite.Pages.Cart;

public class ListModel : PageModel
{
    private readonly ICartService _service;

    public List<CarritoCategoryDto> Cart { get; set; }

    public ListModel(ICartService service)
    {
        Cart = new List<CarritoCategoryDto>();
        _service = service;
    }

    public async Task<IActionResult> OnGet()
    {
        var response = await _service.GetAllAsync();
        Cart = response.Data;
        return Page();
    }
}