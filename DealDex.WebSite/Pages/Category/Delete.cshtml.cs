using DealDex.Api.Dto.Categories;
using DealDex.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DealDex.WebSite.Pages.Category;

public class Delete : PageModel
{
    private readonly ICategoryService _service;

    [BindProperty]
    public CategoryTypeDto CategoryTypeDto { get; set; }

    public List<string> Errors { get; set; } = new List<string>();

    public Delete(ICategoryService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int id)
    {
        CategoryTypeDto = new CategoryTypeDto();
        var response = await _service.GetById(id);
        CategoryTypeDto = response.Data;

        if (CategoryTypeDto == null)
        {
            return RedirectToPage("/Error");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var response = await _service.DeleteAsync(CategoryTypeDto.id);
        return RedirectToPage("./List");
    }
}
