using Dapper;
using Dapper.Contrib.Extensions;
using DealDex.Api.DataAccess.Interfaces;
using DealDex.Core.Entities;
using Tecnm.Ecommerce1.Api.Repositories.Interfecies.Carrito;

namespace Tecnm.Ecommerce1.Api.Repositories.Carrito;

public class CarritoCategoryReposioty : ICarritoCategoryReposioty
{
    private readonly IDbContext _dbContext;

    public CarritoCategoryReposioty(IDbContext context)
    {
        _dbContext = context;
    }
    
    
    public async Task<CarritoCategory> SaveAsycn(CarritoCategory category)
    {
        category.id = await _dbContext.Connection.InsertAsync(category);
        return category;
    }

    public async Task<CarritoCategory> UpdateAsync(CarritoCategory category)
    {
        await _dbContext.Connection.UpdateAsync(category);
        return category;
    }

    public async Task<List<CarritoCategory>> GetAllAsync()
    {
        const string sql = "SELECT * FROM CarritoCategory WHERE isDeleted = 0";

        var categories = await _dbContext.Connection.QueryAsync<CarritoCategory>(sql);
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

    public async Task<CarritoCategory> GetById(int id)
    {
        var category = await _dbContext.Connection.GetAsync<CarritoCategory>(id);
        if (category == null)
            return null;
        return category.IsDeleted == true ? null : category;

    }
}