﻿

using DealDex.Api.Dto;
using DealDex.Core.Entities;
using DealDex.Api.Repositories.Interfecies;
using DealDex.Api.Services.Interfaces;

namespace DealDex.Api.Services;

public class ProductCategoryServices : IProductCategoryService
{
    private readonly IProductCategoryRepository _productCategoryRepository;
    

    public ProductCategoryServices(IProductCategoryRepository productCategoryRepository)
    {
        _productCategoryRepository = productCategoryRepository;
    }
    
    public async Task<bool> ProductCategoryExist(int id)
    {
        var category = await _productCategoryRepository.GetById(id);
        return (category != null);
    }

    public async Task<ProductCategoryDtoAdd> SaveAsycn(ProductCategoryDtoAdd categoryDto)
    {
        var catetegory = new ProductCategory
        {
            IdSupplier = categoryDto.IdSupplier,
            Descripcion = categoryDto.Descripcion,
            IdCategory = categoryDto.IdCategory,
            Estado = categoryDto.Estado,
            Ubicacion = categoryDto.Ubicacion,
            Image = categoryDto.Image,
            Titulo = categoryDto.Titulo,
            Precio = categoryDto.Precio,
            CreatedBy = "Omar",
            CreatedDate = DateTime.Now,
            UpdatedBy = "Omar",
            UpdatedDate = DateTime.Now
        };
        
        catetegory = await _productCategoryRepository.SaveAsycn(catetegory);
        categoryDto.id = catetegory.id;
        return categoryDto;
    }

    public async Task<ProductCategoryDtoAdd> UpdateAsync(ProductCategoryDtoAdd categoryDto)
    {
        var category = await _productCategoryRepository.GetById(categoryDto.id);

        if (category == null)
            throw new Exception("Product Category Not founf");
        category.IdSupplier = categoryDto.IdSupplier;
        category.Descripcion = categoryDto.Descripcion;
        category.IdCategory = categoryDto.IdCategory;
        category.Estado = categoryDto.Estado;
        category.Ubicacion = categoryDto.Ubicacion;
        category.Image = categoryDto.Image;
        category.Titulo = categoryDto.Titulo;
        category.Precio = categoryDto.Precio;
        
        category.UpdatedBy = "Omar";
        category.UpdatedDate = DateTime.Now;
        await _productCategoryRepository.UpdateAsync(category);
        return categoryDto;
    }

    public async Task<List<ProductCategoryDtoAdd>> GetAllAsync()
    {
        var categories = await _productCategoryRepository.GetAllAsync();
        var categoriesDto = categories.Select(c => new ProductCategoryDtoAdd(c)).ToList();
        return categoriesDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _productCategoryRepository.DeleteAsync(id);
    }

    public async Task<ProductCategoryDtoAdd> GetById(int id)
    {
        var category = await _productCategoryRepository.GetById(id);
        if (category == null)
            throw new Exception("Product category not Found");
        var categoryDto = new ProductCategoryDtoAdd(category);
        return categoryDto;
    }

    public async Task<bool> ExistByName(string name, int id = 0)
    {
        var category = await _productCategoryRepository.GetByName(name, id);
        return category != null;
    }
}