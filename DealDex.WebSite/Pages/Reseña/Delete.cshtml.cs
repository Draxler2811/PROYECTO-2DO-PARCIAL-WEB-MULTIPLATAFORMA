using DealDex.Api.Dto.Reseñas;
using DealDex.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DealDex.WebSite.Pages.Reseña;

public class Delete : PageModel
{ 
    [BindProperty] public ReseñaCategoryDto ReseñaCategoryDto { get; set; }
    public List<string> Errors { get; set; } = new List<string>();

    private readonly IReseñaService _service;

    public Delete(IReseñaService service)
    {
        _service = service;
    }
    public async Task<IActionResult> OnGet(int id)
    {
        ReseñaCategoryDto = new ReseñaCategoryDto();
        
        //Obtener informacion
        var response = await _service.GetById(id);
        ReseñaCategoryDto = response.Data;

        if (ReseñaCategoryDto == null)
        {
            return RedirectToPage("./Error");
        }

        return Page();

    }

    public async Task<IActionResult> OnPostAsync()
    {
        var response = await _service.DeleteAsync(ReseñaCategoryDto.id);
        
        return RedirectToPage("./List");
    }
}