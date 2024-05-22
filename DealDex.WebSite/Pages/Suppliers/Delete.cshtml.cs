using DealDex.Api.Dto.Supplier;
using DealDex.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DealDex.WebSite.Pages.Suppliers;

public class Delete : PageModel
{
    [BindProperty] public SupplierInfoDto SupplierInfoDto { get; set; }
    public List<string> Errors { get; set; } = new List<string>();

    private readonly ISupplierService _service;

    public Delete(ISupplierService service)
    {
        _service = service;
    }
    public async Task<IActionResult> OnGet(int id)
    {
        SupplierInfoDto = new SupplierInfoDto();
        
        //Obtener informacion
        var response = await _service.GetById(id);
        SupplierInfoDto = response.Data;

        if (SupplierInfoDto == null)
        {
            return RedirectToPage("./Error");
        }

        return Page();

    }

    public async Task<IActionResult> OnPostAsync()
    {
        var response = await _service.DeleteAsync(SupplierInfoDto.id);
        
        return RedirectToPage("./List");
    }
}