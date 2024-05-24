using DealDex.Api.Dto.Reseñas;
using DealDex.Core.Http;
using DealDex.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DealDex.WebSite.Pages.Reseña;

public class Editar : PageModel
{
    [BindProperty] public ReseñaCategoryDto ReseñaCategoryDto { get; set; }
    public List<string> Errors { get; set; } = new List<string>();

    private readonly IReseñaService _service;

    public Editar(IReseñaService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        ReseñaCategoryDto = new ReseñaCategoryDto();
        if (id.HasValue)
        {
            //Obtener la informacion del servicio
            var response = await _service.GetById(id.Value);
            ReseñaCategoryDto = response.Data;
        }

        if (ReseñaCategoryDto == null)
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

        Response<ReseñaCategoryDto> response;

        
        response = await _service.UpdateAsync(ReseñaCategoryDto);
        
        
        Errors = response.Errors;

        if (Errors.Count > 0)
        {
            return Page();
        }
        return RedirectToPage("./List");
    }
}
