using DealDex.Api.Dto;
using DealDex.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DealDex.WebSite.Pages.Users;

public class ListModel : PageModel
{
    private readonly IUsersService  _service;

    public List<UsersCategoryDto> Users { get; set; }

    public ListModel(IUsersService service)
    {
        Users = new List<UsersCategoryDto>();
        _service = service;
    }

    public async Task<IActionResult> OnGet()
    {
        var response = await _service.GetAllAsync();
        Users = response.Data;
        return Page();
    }
}