using DealDex.Api.Dto.Categories;
using DealDex.Core.Http;
using DealDex.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DealDex.WebSite.Pages.Category;

public class Agregar : PageModel
{
    [BindProperty] public CategoryTypeDto CategoryTypeDto { get; set; }
    public List<string> Errors { get; set; } = new List<string>();

    private readonly ICategoryService _service;

    public Agregar(ICategoryService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        CategoryTypeDto = new CategoryTypeDto();
        if (id.HasValue)
        {
            //Obtener la informacion del servicio
            var response = await _service.GetById(id.Value);
            CategoryTypeDto = response.Data;

        }

        if (CategoryTypeDto == null)
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

        Response<CategoryTypeDto> response;

        //Insercion
        response = await _service.SaveAsync(CategoryTypeDto);

        Errors = response.Errors;

        if (Errors.Count > 0)
        {
            return Page();
        }

        CategoryTypeDto = response.Data;
        return RedirectToPage("./List");
    }
}