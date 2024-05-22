using DealDex.Api.Dto.Reseñas;
using DealDex.Api.Repositories.Interfecies;
using DealDex.Core.Entities;
using Tecnm.Ecommerce1.Api.Repositories.Interfecies.Reseñas;
using Tecnm.Ecommerce1.Api.Services.Interfaces.Reseñas;

namespace Tecnm.Ecommerce1.Api.Services.Reseñas;

public class ReseñasCategoryServices : IReseñasCategoryServices
{
    private readonly IReseñaCategoryRepository _reseñaCategoryRepository;
    

    public ReseñasCategoryServices(IReseñaCategoryRepository reseñaCategoryRepository)
    {
        _reseñaCategoryRepository = reseñaCategoryRepository;
    }
    
    public  async Task<bool> ReseñaCategoryExist(int id)
    {
        var category = await _reseñaCategoryRepository.GetById(id);
        return (category != null);
    }

    public async Task<ReseñaCategoryDto> SaveAsycn(ReseñaCategoryDto categoryDto)
    {
        var catetegory = new ReseñaCategory
        {
            IdProducto = categoryDto.IdProducto,
            Titulo = categoryDto.Titulo,
            Valoracion = categoryDto.Valoracion,
            CreatedBy = "Omar",
            CreatedDate = DateTime.Now,
            UpdatedBy = "Omar",
            UpdatedDate = DateTime.Now
        };
        
        catetegory = await _reseñaCategoryRepository.SaveAsycn(catetegory);
        categoryDto.id = catetegory.id;
        return categoryDto;
    }

    public  async Task<ReseñaCategoryDto> UpdateAsync(ReseñaCategoryDto categoryDto)
    {
        var category = await _reseñaCategoryRepository.GetById(categoryDto.id);

        if (category == null)
            throw new Exception("Product Category Not founf");
        category.IdProducto = categoryDto.IdProducto;
        category.Titulo = categoryDto.Titulo;
        category.Valoracion = categoryDto.Valoracion;     
        category.UpdatedBy = "Omar";
        category.UpdatedDate = DateTime.Now;
        await _reseñaCategoryRepository.UpdateAsync(category);
        return categoryDto;
    }

    public async Task<List<ReseñaCategoryDto>> GetAllAsync()
    {
        var reseña = await _reseñaCategoryRepository.GetAllAsync();
        var categoriesDto = reseña.Select(c => new ReseñaCategoryDto(c)).ToList();
        return categoriesDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _reseñaCategoryRepository.DeleteAsync(id);
    }

    public  async Task<ReseñaCategoryDto> GetById(int id)
    {
        var category = await _reseñaCategoryRepository.GetById(id);
        if (category == null)
            throw new Exception("Product category not Found");
        var categoryDto = new ReseñaCategoryDto(category);
        return categoryDto;
    }

    public async Task<bool> ExistByName(string name, int id = 0)
    {
        var category = await _reseñaCategoryRepository.GetByName(name, id);
        return category != null;
    }
}