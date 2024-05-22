
using DealDex.Api.Dto;
using DealDex.Api.Dto.Carrito;
using DealDex.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DealDex.WebSite.Pages.Cart;

public class Delete : PageModel
{
    private readonly ICartService _service;

    [BindProperty]
    public CarritoCategoryDto CarritoDto { get; set; }

    public List<string> Errors { get; set; } = new List<string>();

    public Delete(ICartService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int id)
    {
        CarritoDto = new CarritoCategoryDto();
        var response = await _service.GetById(id);
        CarritoDto = response.Data;

        if (CarritoDto == null)
        {
            return RedirectToPage("/Error");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var response = await _service.DeleteAsync(CarritoDto.id);
        return RedirectToPage("./List");
    }
}
