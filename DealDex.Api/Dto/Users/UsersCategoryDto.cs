using DealDex.Core.Entities;

namespace DealDex.Api.Dto;

public class UsersCategoryDto : DtoBase
{
    public string Correo { get; set; }
    public string Contraseña { get; set; }
    public string NombreUsu { get; set; }
    


    public UsersCategoryDto()
    {
        
    }

    public UsersCategoryDto(UsersCategory category)
    {
        id = category.id;
        Correo = category.Correo;
        Contraseña = category.Contraseña;
        NombreUsu = category.NombreUsu;
    }
}