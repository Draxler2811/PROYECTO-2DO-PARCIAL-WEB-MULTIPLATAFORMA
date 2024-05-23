using DealDex.Api.Dto.Supplier;
using DealDex.Core.Http;
using DealDex.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DealDex.WebSite.Pages.Suppliers;

public class Editar : PageModel
{
    [BindProperty] public SupplierInfoDto SupplierInfoDto { get; set; }
    public List<string> Errors { get; set; } = new List<string>();

    private readonly ISupplierService _service;

    public Editar(ISupplierService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        SupplierInfoDto = new SupplierInfoDto();
        if (id.HasValue)
        {
            //Obtener la informacion del servicio
            var response = await _service.GetById(id.Value);
            SupplierInfoDto = response.Data;
        }

        if (SupplierInfoDto == null)
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

        Response<SupplierInfoDto> response;

        
        response = await _service.UpdateAsync(SupplierInfoDto);
        
        
        

        SupplierInfoDto = response.Data;
        return RedirectToPage("./List");
    }
}
