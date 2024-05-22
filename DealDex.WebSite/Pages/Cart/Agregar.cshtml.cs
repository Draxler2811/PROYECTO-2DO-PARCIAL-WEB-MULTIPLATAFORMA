using DealDex.Api.Dto;
using DealDex.Api.Dto.Carrito;
using DealDex.Core.Http;
using DealDex.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DealDex.WebSite.Pages.Cart;

public class Agregar : PageModel
{
    [BindProperty] public CarritoCategoryDto CarritoCategoryDto { get; set; }
    public List<string> Errors { get; set; } = new List<string>();

    private readonly ICartService _service;

    public Agregar(ICartService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        CarritoCategoryDto = new CarritoCategoryDto();
        if (id.HasValue)
        {
            //Obtener la informacion del servicio
            var response = await _service.GetById(id.Value);
            CarritoCategoryDto = response.Data;

        }

        if (CarritoCategoryDto == null)
        {
            return RedirectToPage("/Error");
        }

        return Page();

    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();

        }

        Response<CarritoCategoryDto> response;

        //Insercion
        response = await _service.SaveAsync(CarritoCategoryDto);

        Errors = response.Errors;

        if (Errors.Count > 0)
        {
            return Page();
        }

        CarritoCategoryDto = response.Data;
        return RedirectToPage("./List");
    }
}