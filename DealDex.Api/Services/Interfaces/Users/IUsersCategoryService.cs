using DealDex.Api.Dto;
using DealDex.Core.Entities;

namespace Tecnm.Ecommerce1.Api.Services.Interfaces.Users;

public interface IUsersCategoryService
{
    Task<bool> UsersCategoryExist(int id);
    
    Task<UsersCategoryDto> SaveAsycn(UsersCategoryDto category);
    
    Task<UsersCategoryDto> UpdateAsync(UsersCategoryDto category);
    
    Task<List<UsersCategoryDto>> GetAllAsync();
    
    Task<bool> DeleteAsync(int id);
    
    Task<UsersCategoryDto> GetById(int id);
    
    Task<bool> ValidateCredentials(string correo, string contraseña);


    
}