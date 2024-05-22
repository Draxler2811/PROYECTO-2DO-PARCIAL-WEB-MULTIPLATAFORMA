using DealDex.Api.Dto.Favoritos;
using DealDex.WebSite.Services;
using DealDex.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DealDex.WebSite.Pages.Favorite;

public class Delete : PageModel
{
    [BindProperty] public FavoriteProductDto FavoriteProductDto { get; set; }
    public List<string> Errors { get; set; } = new List<string>();

    private readonly IFavoriteService _service;

    public Delete(IFavoriteService service)
    {
        _service = service;
    }
    public async Task<IActionResult> OnGet(int id)
    {
        FavoriteProductDto = new FavoriteProductDto();
        
        //Obtener informacion
        var response = await _service.GetById(id);
        FavoriteProductDto = response.Data;

        if (FavoriteProductDto == null)
        {
            return RedirectToPage("./Error");
        }

        return Page();

    }

    public async Task<IActionResult> OnPostAsync()
    {
        var response = await _service.DeleteAsync(FavoriteProductDto.id);
        
        return RedirectToPage("./List");
    }
}