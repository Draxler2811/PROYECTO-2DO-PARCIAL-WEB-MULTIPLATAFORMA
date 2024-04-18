using Dapper;
using Dapper.Contrib.Extensions;
using DealDex.Api.DataAccess.Interfaces;
using DealDex.Core.Entities;
using Tecnm.Ecommerce1.Api.Repositories.Interfecies.Reseñas;

namespace Tecnm.Ecommerce1.Api.Repositories.Reseñas;

public class ReseñaCategoryRepository: IReseñaCategoryRepository
{
    
    private readonly IDbContext _dbContext;

    public ReseñaCategoryRepository(IDbContext context)
    {
        _dbContext = context;
    }
    
    public async Task<ReseñaCategory> SaveAsycn(ReseñaCategory category)
    {
        category.id = await _dbContext.Connection.InsertAsync(category);
        return category;
    }

    public  async Task<ReseñaCategory> UpdateAsync(ReseñaCategory category)
    {
        await _dbContext.Connection.UpdateAsync(category);
        return category;
    }

    public  async Task<List<ReseñaCategory>> GetAllAsync()
    {
        const string sql = "SELECT * FROM ReseñaCategory WHERE isDeleted = 0";

        var categories = await _dbContext.Connection.QueryAsync<ReseñaCategory>(sql);
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

    public async Task<ReseñaCategory> GetById(int id)
    {
        var category = await _dbContext.Connection.GetAsync<ReseñaCategory>(id);
        if (category == null)
            return null;
        return category.IsDeleted == true ? null : category;

    }
}