using DealDex.Api.Dto.Reseñas;
using DealDex.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DealDex.WebSite.Pages.Reseña;

public class ListModel : PageModel
{
    private readonly IReseñaService _service;

    public List<ReseñaCategoryDto> Reseña { get; set; }

    public ListModel(IReseñaService service)
    {
        Reseña = new List<ReseñaCategoryDto>();
        _service = service;
    }

    public async Task<IActionResult> OnGet()
    {
        var response = await _service.GetAllAsync();
        Reseña = response.Data;
        return Page();
    }
}