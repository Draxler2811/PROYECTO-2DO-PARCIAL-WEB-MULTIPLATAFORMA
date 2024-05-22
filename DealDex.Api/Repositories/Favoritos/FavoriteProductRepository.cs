using Dapper;
using Dapper.Contrib.Extensions;
using DealDex.Api.DataAccess.Interfaces;
using DealDex.Core.Entities;
using Tecnm.Ecommerce1.Api.Repositories.Interfecies.Favoritos;

namespace Tecnm.Ecommerce1.Api.Repositories.Favoritos;

public class FavoriteProductRepository : IFavoriteProductRepository
{
    private readonly IDbContext _dbContext;

    public FavoriteProductRepository(IDbContext context)
    {
        _dbContext = context;
    }
    
    
    public async Task<FavoriteProduct> SaveAsycn(FavoriteProduct category)
    {
        category.id = await _dbContext.Connection.InsertAsync(category);
        return category;    }

    public async Task<FavoriteProduct> UpdateAsync(FavoriteProduct category)
    {
        await _dbContext.Connection.UpdateAsync(category);
        return category;
    }

    public async Task<List<FavoriteProduct>> GetAllAsync()
    {
        const string sql = "SELECT * FROM FavoriteProduct WHERE isDeleted = 0";

        var categories = await _dbContext.Connection.QueryAsync<FavoriteProduct>(sql);
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

    public async Task<FavoriteProduct> GetById(int id)
    {
        var category = await _dbContext.Connection.GetAsync<FavoriteProduct>(id);
        if (category == null)
            return null;
        return category.IsDeleted == true ? null : category;

    }

    public async Task<FavoriteProduct> GetByName(string name, int id = 0)
    {
        string sql = $"SELECT * FROM FavoriteProduct WHERE Titulo = '{name}' AND id <> {id}";
        var categories = await _dbContext.Connection.QueryAsync<FavoriteProduct>(sql);

        return categories.ToList().FirstOrDefault();
    }
}