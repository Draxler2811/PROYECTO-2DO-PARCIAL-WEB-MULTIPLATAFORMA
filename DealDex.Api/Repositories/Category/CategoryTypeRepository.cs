using Dapper;
using Dapper.Contrib.Extensions;
using DealDex.Api.DataAccess.Interfaces;
using DealDex.Core.Entities;
using Tecnm.Ecommerce1.Api.Repositories.Interfecies.Category;

namespace Tecnm.Ecommerce1.Api.Repositories.Compras;

public class CategoryTypeRepository :ICategoryTypeRepository
{
    private readonly IDbContext _dbContext;

    public CategoryTypeRepository(IDbContext context)
    {
        _dbContext = context;
    }
    
    
    public async Task<CategoryType> SaveAsycn(CategoryType category)
    {
        category.id = await _dbContext.Connection.InsertAsync(category);
        return category;
        
    }

    public async Task<CategoryType> UpdateAsync(CategoryType category)
    {
        await _dbContext.Connection.UpdateAsync(category);
        return category;
    }

    public async Task<List<CategoryType>> GetAllAsync()
    {
        const string sql = "SELECT * FROM CategoryType WHERE isDeleted = 0";

        var categories = await _dbContext.Connection.QueryAsync<CategoryType>(sql);
        return categories.ToList();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await GetById(id);
        if (category == null)
            return false;

        category.IsDeleted = true;

        return await _dbContext.Connection.UpdateAsync(category);
    }

    public async Task<CategoryType> GetById(int id)
    {
        var category = await _dbContext.Connection.GetAsync<CategoryType>(id);
        if (category == null)
            return null;
        return category.IsDeleted == true ? null : category;
    }
}