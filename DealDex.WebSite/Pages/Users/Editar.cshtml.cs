using DealDex.Api.Dto;
using DealDex.Core.Http;
using DealDex.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DealDex.WebSite.Pages.Users;

public class Editar : PageModel
{
    [BindProperty] public UsersCategoryDto UsersCategoryDto { get; set; }
    public List<string> Errors { get; set; } = new List<string>();

    private readonly IUsersService _service;

    public Editar(IUsersService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        UsersCategoryDto = new UsersCategoryDto();
        if (id.HasValue)
        {
            //Obtener la informacion del servicio
            var response = await _service.GetById(id.Value);
            UsersCategoryDto = response.Data;
        }

        if (UsersCategoryDto == null)
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

        Response<UsersCategoryDto> response;

        
        response = await _service.UpdateAsync(UsersCategoryDto);
        
        
        

        UsersCategoryDto = response.Data;
        return RedirectToPage("./List");
    }
}
