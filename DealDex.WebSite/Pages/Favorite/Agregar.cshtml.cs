using DealDex.Api.Dto.Favoritos;
using DealDex.Core.Entities;
using DealDex.Core.Http;
using DealDex.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DealDex.WebSite.Pages.Favorite;

public class Agregar : PageModel
{
    [BindProperty] public FavoriteProductDto FavoriteProduct { get; set; }
    public List<string> Errors { get; set; } = new List<string>();

    private readonly IFavoriteService _service;

    public Agregar(IFavoriteService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        FavoriteProduct = new FavoriteProductDto();
        if (id.HasValue)
        {
            //Obtener la informacion del servicio
            var response = await _service.GetById(id.Value);
            FavoriteProduct = response.Data;

        }

        if (FavoriteProduct == null)
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

        Response<FavoriteProductDto> response;

        //Insercion
        response = await _service.SaveAsync(FavoriteProduct);

        Errors = response.Errors;

        if (Errors.Count > 0)
        {
            return Page();
        }

        FavoriteProduct = response.Data;
        return RedirectToPage("./List");
    }
}