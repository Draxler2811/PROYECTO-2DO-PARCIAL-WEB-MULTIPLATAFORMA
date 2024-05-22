using DealDex.Api.Dto.Supplier;
using DealDex.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DealDex.WebSite.Pages.Suppliers;

public class ListModel : PageModel
{
    private readonly ISupplierService _service;

    public List<SupplierInfoDto> Supplier { get; set; }

    public ListModel(ISupplierService service)
    {
        Supplier = new List<SupplierInfoDto>();
        _service = service;
    }

    public async Task<IActionResult> OnGet()
    {
        var response = await _service.GetAllAsync();
        Supplier = response.Data;
        return Page();
    }
}