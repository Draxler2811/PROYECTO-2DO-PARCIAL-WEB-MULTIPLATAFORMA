using DealDex.Api.Dto;
using DealDex.Core.Http;

namespace DealDex.WebSite.Services.Interfaces;

public interface IUsersService
{
    Task<Response<List<UsersCategoryDto>>> GetAllAsync();
    
    Task<Response<UsersCategoryDto>> GetById(int id);
    
    Task<Response<UsersCategoryDto>> SaveAsync(UsersCategoryDto usersDto);
    
    Task<Response<UsersCategoryDto>> UpdateAsync(UsersCategoryDto usersDto);

    Task<Response<bool>> DeleteAsync(int id);

    Task<bool> ValidateCredentials(string correo, string contraseña);

}