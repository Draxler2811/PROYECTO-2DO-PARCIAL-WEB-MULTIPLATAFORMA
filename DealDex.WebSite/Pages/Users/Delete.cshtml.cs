using DealDex.Api.Dto;
using DealDex.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DealDex.WebSite.Pages.Users;

public class Delete : PageModel
{
    [BindProperty] public UsersCategoryDto UsersCategoryDto { get; set; }
    public List<string> Errors { get; set; } = new List<string>();

    private readonly IUsersService _service;

    public Delete(IUsersService service)
    {
        _service = service;
    }
    public async Task<IActionResult> OnGet(int id)
    {
        UsersCategoryDto = new UsersCategoryDto();
        
        //Obtener informacion
        var response = await _service.GetById(id);
        UsersCategoryDto = response.Data;

        if (UsersCategoryDto == null)
        {
            return RedirectToPage("./Error");
        }

        return Page();

    }

    public async Task<IActionResult> OnPostAsync()
    {
        var response = await _service.DeleteAsync(UsersCategoryDto.id);
        
        return RedirectToPage("./List");
    }
}