using DealDex.Api.Dto.Favoritos;
using DealDex.Core.Http;
using DealDex.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DealDex.WebSite.Pages.Favorite;

public class Editar : PageModel
{
    [BindProperty] public FavoriteProductDto FavoriteProductDto { get; set; }
    public List<string> Errors { get; set; } = new List<string>();

    private readonly IFavoriteService _service;

    public Editar(IFavoriteService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        FavoriteProductDto = new FavoriteProductDto();
        if (id.HasValue)
        {
            //Obtener la informacion del servicio
            var response = await _service.GetById(id.Value);
            FavoriteProductDto = response.Data;
        }

        if (FavoriteProductDto == null)
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

        
        response = await _service.UpdateAsync(FavoriteProductDto);
        
        
        

        FavoriteProductDto = response.Data;
        return RedirectToPage("./List");
    }
}
