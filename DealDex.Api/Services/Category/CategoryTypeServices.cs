using DealDex.Api.Dto.Categories;
using DealDex.Core.Entities;
using Tecnm.Ecommerce1.Api.Repositories.Interfecies.Category;
using Tecnm.Ecommerce1.Api.Services.Interfaces.category;

namespace Tecnm.Ecommerce1.Api.Services.Category;

public class CategoryTypeServices : ICategoryTypeServices
{
    private readonly ICategoryTypeRepository _categoryTypeRepository;
    

    public CategoryTypeServices(ICategoryTypeRepository categoryTypeRepository)
    {
        _categoryTypeRepository = categoryTypeRepository;
    }

    
    public async Task<bool> CategoryTypeExist(int id)
    {
        var category = await  _categoryTypeRepository.GetById(id);
        return (category != null);
    }

    public async Task<CategoryTypeDto> SaveAsyc(CategoryTypeDto categoryTypeDto)
    {
        var category = new CategoryType
        {
            Nombre = categoryTypeDto.Nombre,
            CreatedBy = "Omar",
            CreatedDate = DateTime.Now,
            UpdatedBy = "Omar",
            UpdatedDate = DateTime.Now
        };
        
        category = await  _categoryTypeRepository.SaveAsycn(category);
        categoryTypeDto.id = category.id;
        return categoryTypeDto;
    }

    public async Task<CategoryTypeDto> UpdateAsync(CategoryTypeDto categoryTypeDto)
    {
        var category = await  _categoryTypeRepository.GetById(categoryTypeDto.id);

        if (category == null)
            throw new Exception("Product Category Not founf");
        category.Nombre = categoryTypeDto.Nombre;
        category.UpdatedBy = "Omar";
        category.UpdatedDate = DateTime.Now;
        await  _categoryTypeRepository.UpdateAsync(category);
        return categoryTypeDto;
    }

    public async Task<List<CategoryTypeDto>> GetAllAsync()
    {
        var category = await  _categoryTypeRepository.GetAllAsync();
        var categoryDto = category.Select(c => new CategoryTypeDto(c)).ToList();
        return categoryDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await  _categoryTypeRepository.DeleteAsync(id);
    }

    public async Task<CategoryTypeDto> GetById(int id)
    {
        var category = await _categoryTypeRepository.GetById(id);
        if (category == null)
            throw new Exception("Product category not Found");
        var categoryDto = new CategoryTypeDto(category);
        return categoryDto;
    }
    

    public async Task<bool> ExistByName(string name, int id = 0)
    {
        var category = await _categoryTypeRepository.GetByName(name, id);
        return category != null;
    }
}