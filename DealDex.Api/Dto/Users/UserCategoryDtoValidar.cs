using DealDex.Core.Entities;

namespace DealDex.Api.Dto;

public class UserCategoryDtoValidar 
{
    public string Correo { get; set; }
    public string Contraseña { get; set; }
    
    public UserCategoryDtoValidar()
    {
        
    }

    public UserCategoryDtoValidar(UsersCategory category)
    {
        Correo = category.Correo;
        Contraseña = category.Contraseña;
    }
}