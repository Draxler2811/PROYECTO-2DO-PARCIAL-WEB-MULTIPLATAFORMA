using DealDex.Api.Dto;
using DealDex.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DealDex.WebSite.Pages.Login;

public class Login : PageModel
{
    [BindProperty] public UserCategoryDtoValidar UsersCategoryDto { get; set; }
    public List<string> Errors { get; set; } = new List<string>();

    private readonly IUsersService _service;

    public Login(IUsersService service)
    {
        _service = service;
    }
    public void OnGet()
    {
        
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            Errors.Add("Todos los campos son obligatorios.");
            return Page();
        }

        var isValid = await _service.ValidateCredentials(UsersCategoryDto.Correo, UsersCategoryDto.Contraseña);
        if (isValid)
        {
            // Redirigir a la página principal o cualquier otra página protegida
            return RedirectToPage("/Index");
        }
        else
        {
            Errors.Add("Usuario o contraseña incorrectos.");
            return Page();
        }
    }
}


    