using DealDex.Api.Dto;
using DealDex.Api.Repositories.Interfecies;
using DealDex.Core.Entities;
using Tecnm.Ecommerce1.Api.Services.Interfaces.Users;

namespace Tecnm.Ecommerce1.Api.Services.Users;

public class UsersCategoryService : IUsersCategoryService
{
    private readonly IUsersCategoryRepository _usersCategoryRepository;


    public UsersCategoryService(IUsersCategoryRepository usersCategoryRepository)
    {
        _usersCategoryRepository = usersCategoryRepository;
    }


    public async Task<bool> UsersCategoryExist(int id)
    {
        var category = await _usersCategoryRepository.GetById(id);
        return (category != null);
    }

    public async Task<UsersCategoryDto> SaveAsycn(UsersCategoryDto categoryDto)
    {
        var catetegory = new UsersCategory()
        {
            Correo = categoryDto.Correo,
            Contraseña = categoryDto.Contraseña,
            NombreUsu = categoryDto.NombreUsu,
            CreatedBy = "Omar",
            CreatedDate = DateTime.Now,
            UpdatedBy = "Omar",
            UpdatedDate = DateTime.Now
        };

        catetegory = await _usersCategoryRepository.SaveAsycn(catetegory);
        categoryDto.id = catetegory.id;
        return categoryDto;
    }

    public async Task<UsersCategoryDto> UpdateAsync(UsersCategoryDto categoryDto)
    {
        var category = await _usersCategoryRepository.GetById(categoryDto.id);

        if (category == null)
            throw new Exception("Product Category Not founf");
        category.Correo = categoryDto.Correo;
        category.Contraseña = categoryDto.Contraseña;
        category.NombreUsu = categoryDto.NombreUsu;
        category.UpdatedBy = "Omar";
        category.UpdatedDate = DateTime.Now;
        await _usersCategoryRepository.UpdateAsync(category);
        return categoryDto;
    }

    public async Task<List<UsersCategoryDto>> GetAllAsync()
    {
        var categories = await _usersCategoryRepository.GetAllAsync();
        var categoriesDto = categories.Select(c => new UsersCategoryDto(c)).ToList();
        return categoriesDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _usersCategoryRepository.DeleteAsync(id);
    }

    public async Task<UsersCategoryDto> GetById(int id)
    {
        var category = await _usersCategoryRepository.GetById(id);
        if (category == null)
            throw new Exception("Product category not Found");
        var categoryDto = new UsersCategoryDto()
        {
            Correo = category.Correo,
            Contraseña = category.Contraseña,
            NombreUsu = category.NombreUsu,
        };
        return categoryDto;
    }

    public async Task<bool> ValidateCredentials(string correo, string contraseña)
    {
        var user = await _usersCategoryRepository.GetUserByEmailAndPassword(correo, contraseña);
        return user != null;
    }

}