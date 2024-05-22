using DealDex.Api.Dto.Reseñas;
using DealDex.Core.Http;
using DealDex.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DealDex.WebSite.Pages.Reseña;

public class Agregar : PageModel
{
    [BindProperty] public ReseñaCategoryDto ReseñaCategoryDto { get; set; }
    public List<string> Errors { get; set; } = new List<string>();

    private readonly IReseñaService _service;

    public Agregar(IReseñaService service)
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

        //Insercion
        response = await _service.SaveAsync(ReseñaCategoryDto);

        Errors = response.Errors;

        if (Errors.Count > 0)
        {
            return Page();
        }

        ReseñaCategoryDto = response.Data;
        return RedirectToPage("./List");
    }
}