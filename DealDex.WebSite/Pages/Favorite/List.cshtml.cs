using DealDex.Api.Dto.Favoritos;
using DealDex.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DealDex.WebSite.Pages.Favorite;

public class ListModel : PageModel
{ private readonly IFavoriteService _service;

    public List<FavoriteProductDto> FavoriteProductDtos { get; set; }

    public ListModel(IFavoriteService service)
    {
        FavoriteProductDtos = new List<FavoriteProductDto>();
        _service = service;
    }

    public async Task<IActionResult> OnGet()
    {
        var response = await _service.GetAllAsync();
        FavoriteProductDtos = response.Data;
        return Page();
    }
}