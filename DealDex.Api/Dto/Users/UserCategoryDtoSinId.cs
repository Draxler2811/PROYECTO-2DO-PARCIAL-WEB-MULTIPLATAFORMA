using DealDex.Core.Entities;

namespace DealDex.Api.Dto;

public class UserCategoryDtoSinId
{
    public string Correo { get; set; }
    public string Contraseña { get; set; }
    public string NombreUsu { get; set; }
    


    public UserCategoryDtoSinId()
    {
        
    }

    public UserCategoryDtoSinId(UsersCategory category)
    {
        Correo = category.Correo;
        Contraseña = category.Contraseña;
        NombreUsu = category.NombreUsu;
    }
}