using DealDex.Api.Dto;
using DealDex.Core.Http;
using DealDex.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DealDex.WebSite.Pages.Product;

public class Agregar : PageModel
{
    [BindProperty] public ProductCategoryDtoAdd ProductCategoryDtoAdd { get; set; }
    public List<string> Errors { get; set; } = new List<string>();

    private readonly IProductService _service;

    public Agregar(IProductService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        ProductCategoryDtoAdd = new ProductCategoryDtoAdd();
        if (id.HasValue)
        {
            //Obtener la informacion del servicio
            var response = await _service.GetById(id.Value);
            ProductCategoryDtoAdd = response.Data;

        }

        if (ProductCategoryDtoAdd == null)
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

        Response<ProductCategoryDtoAdd> response;

        //Insercion
        response = await _service.SaveAsync(ProductCategoryDtoAdd);

        Errors = response.Errors;

        if (Errors.Count > 0)
        {
            return Page();
        }

        ProductCategoryDtoAdd = response.Data;
        return RedirectToPage("./List");
    }
}